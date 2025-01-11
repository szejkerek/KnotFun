using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MapGenerator
{
    private static MapConfigSO mapConfig;
    private static List<List<MapNode>> mapNodes = new List<List<MapNode>>();

    private static List<List<Point>> paths;

    public static Map CreateMap(MapConfigSO config)
    {
        if (config == null)
        {
            Debug.LogWarning("mapConfig was null");
            return null;
        }
        mapConfig = config;
        mapNodes.Clear();



        for (int i = 0; i < mapConfig.mapLayers.Count; i++)
            mapNodes.Add(CreateNodesForLayer(i, mapConfig.mapGridWidth));

        GeneratePaths();
        SetUpConnections();
        RemoveCrossConnections();

        List<MapNode> nodes = mapNodes.SelectMany(n => n).Where(n => n.inNodesLocations.Count > 0 || n.outNodesLocations.Count > 0).ToList();
        return new Map(config.name, nodes, new List<Point>());
    }



    private static List<MapNode> CreateNodesForLayer(int layer, int numOfNodes)
    {
        MapLayer mapLayer = mapConfig.mapLayers[layer];
        if (mapLayer == null)
        {
            Debug.LogWarning("mapConfig layer 0 was null");
            return null;
        }

        List<MapNode> mapNodes = new List<MapNode>();

        for (int i = 0; i < numOfNodes; i++)
        {
            Point location = new Point(i, layer);
            mapNodes.Add(new MapNode(location, mapLayer.DrawRandomNodeType())
            {
                //position = new Vector2(
                //math.remap(0, mapConfig.mapLayers.Count - 1, 0f, 1f, location.y),
                // math.remap(0, mapConfig.mapGridWidth - 1, 0f, 1f, location.x))
            });
        }

        return mapNodes;
    }


    private static MapNode GetNode(Point p)
    {
        if (p.y >= mapNodes.Count) return null;
        if (p.x >= mapNodes[p.y].Count) return null;

        return mapNodes[p.y][p.x];
    }

    private static Point GetFinalNode()
    {
        var y = mapConfig.mapLayers.Count - 1;
        if (mapConfig.mapGridWidth % 2 == 1)
            return new Point(mapConfig.mapGridWidth / 2, y);

        return UnityEngine.Random.Range(0, 2) == 0
            ? new Point(mapConfig.mapGridWidth / 2, y)
            : new Point(mapConfig.mapGridWidth / 2 - 1, y);
    }

    private static void GeneratePaths()
    {
        var finalNode = GetFinalNode();
        paths = new List<List<Point>>();
        var numOfStartingNodes = mapConfig.numOfStartingNodes;
        var numOfPreBossNodes = mapConfig.numOfPreBossNodes;

        var candidateXs = new List<int>();
        for (var i = 0; i < mapConfig.mapGridWidth; i++)
            candidateXs.Add(i);

        candidateXs.Shuffle();
        var startingXs = candidateXs.Take(numOfStartingNodes);
        var startingPoints = (from x in startingXs select new Point(x, 0)).ToList();

        candidateXs.Shuffle();
        var preBossXs = candidateXs.Take(numOfPreBossNodes);
        var preBossPoints = (from x in preBossXs select new Point(x, finalNode.y - 1)).ToList();

        int numOfPaths = Mathf.Max(numOfStartingNodes, numOfPreBossNodes) + Mathf.Max(0, mapConfig.extraPaths); ;
        for (int i = 0; i < numOfPaths; ++i)
        {
            Point startNode = startingPoints[i % numOfStartingNodes];
            Point endNode = preBossPoints[i % numOfPreBossNodes];
            var path = Path(startNode, endNode);
            path.Add(finalNode);
            paths.Add(path);
        }
    }

    private static List<Point> Path(Point fromPoint, Point toPoint)
    {
        int toRow = toPoint.y;
        int toCol = toPoint.x;

        int lastNodeCol = fromPoint.x;

        var path = new List<Point> { fromPoint };
        var candidateCols = new List<int>();
        for (int row = 1; row < toRow; ++row)
        {
            candidateCols.Clear();

            int verticalDistance = toRow - row;
            int horizontalDistance;

            int forwardCol = lastNodeCol;
            horizontalDistance = Mathf.Abs(toCol - forwardCol);
            if (horizontalDistance <= verticalDistance)
                candidateCols.Add(lastNodeCol);

            int leftCol = lastNodeCol - 1;
            horizontalDistance = Mathf.Abs(toCol - leftCol);
            if (leftCol >= 0 && horizontalDistance <= verticalDistance)
                candidateCols.Add(leftCol);

            int rightCol = lastNodeCol + 1;
            horizontalDistance = Mathf.Abs(toCol - rightCol);
            if (rightCol < mapConfig.mapGridWidth && horizontalDistance <= verticalDistance)
                candidateCols.Add(rightCol);

            int RandomCandidateIndex = UnityEngine.Random.Range(0, candidateCols.Count);
            int candidateCol = candidateCols[RandomCandidateIndex];
            var nextPoint = new Point(candidateCol, row);

            path.Add(nextPoint);

            lastNodeCol = candidateCol;
        }

        path.Add(toPoint);

        return path;
    }

    private static void SetUpConnections()
    {
        foreach (var path in paths)
        {
            for (var i = 0; i < path.Count - 1; ++i)
            {
                var node = GetNode(path[i]);
                var nextNode = GetNode(path[i + 1]);
                node.AddOutNode(nextNode.locationOnMap);
                nextNode.AddInNode(node.locationOnMap);
            }
        }
    }

    private static void RemoveCrossConnections()
    {
        for (var i = 0; i < mapConfig.mapGridWidth - 1; ++i)
            for (var j = 0; j < mapConfig.mapLayers.Count - 1; ++j)
            {
                var node = GetNode(new Point(i, j));
                if (node == null || node.HasNoConnections()) continue;
                var right = GetNode(new Point(i + 1, j));
                if (right == null || right.HasNoConnections()) continue;
                var top = GetNode(new Point(i, j + 1));
                if (top == null || top.HasNoConnections()) continue;
                var topRight = GetNode(new Point(i + 1, j + 1));
                if (topRight == null || topRight.HasNoConnections()) continue;

                if (!node.outNodesLocations.Any(element => element.Equals(topRight.locationOnMap))) continue;
                if (!right.outNodesLocations.Any(element => element.Equals(top.locationOnMap))) continue;

                node.AddOutNode(top.locationOnMap);
                top.AddInNode(node.locationOnMap);

                right.AddOutNode(topRight.locationOnMap);
                topRight.AddInNode(right.locationOnMap);

                var rnd = UnityEngine.Random.Range(0f, 1f);
                if (rnd < 0.2f)
                {
                    node.RemoveOutNode(topRight.locationOnMap);
                    topRight.RemoveInNode(node.locationOnMap);

                    right.RemoveOutNode(top.locationOnMap);
                    top.RemoveInNode(right.locationOnMap);
                }
                else if (rnd < 0.6f)
                {
                    node.RemoveOutNode(topRight.locationOnMap);
                    topRight.RemoveInNode(node.locationOnMap);
                }
                else
                {
                    right.RemoveOutNode(top.locationOnMap);
                    top.RemoveInNode(right.locationOnMap);
                }
            }
    }
}
