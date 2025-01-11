using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class MapDrawerUI : Singleton<MapDrawerUI>
{
    public MapManager mapManager;
    public MapConfigSO mapConfigs;
    public Map map;
    public GameObject contentParent;
    public GameObject mapCanvas;
    public GameObject mapNodeUIPrefab;
    public RectTransform rectTransform;
    private float contentX, contentY;

    [Header("Line Settings")]
    public GameObject linePrefab;
    [SerializeField] private UILineRenderer uiLinePrefab;
    [Tooltip("Line point count should be > 2 to get smooth color gradients")]
    [Range(3, 10)]
    public int linePointsCount = 10;
    [Tooltip("Distance from the node till the line starting point")]
    public float offsetFromNodes = 0.5f;

    [Header("Colors")]
    [Tooltip("Visited node color")]
    public Color32 visitedColor = Color.white;
    [Tooltip("Attainable node color")]
    public Color32 attainableColor = Color.yellow;
    [Tooltip("locked node color")]
    public Color32 lockedColor = Color.gray;
    [Tooltip("Visited path color")]
    public Color32 lineVisitedColor = Color.white;
    [Tooltip("Attainable path color")]
    public Color32 lineAttainableColor = Color.yellow;
    [Tooltip("Unavailable path color")]
    public Color32 lineLockedColor = Color.gray;


    public readonly List<MapNodeUI> MapNodesUI = new List<MapNodeUI>();
    protected readonly List<LineConnection> lineConnections = new List<LineConnection>();
    public void ShowMap(Map map, MapConfigSO config)
    {
        Clear();
        contentParent = new GameObject("MapParent");
        rectTransform = contentParent.AddComponent<RectTransform>();
        SetPropertise();
        contentParent.transform.SetParent(mapCanvas.transform, false);

        mapConfigs = config;
        contentX = rectTransform.rect.width;
        contentY = rectTransform.rect.height;
        if (map == null)
        {
            Debug.LogWarning("Map was null in MapView.ShowMap()");
            return;
        }

        foreach (MapNode mapNode in map.mapNodes)
        {
            GenerateMapNodeUI(mapNode);
        }
        DrawLines();
        SetAttainableNodes();
        SetLineColors();


        this.map = map;
    }

    private void SetPropertise()
    {
        rectTransform.offsetMin = new Vector2(50, 170);
        rectTransform.offsetMax = new Vector2(-50, -50);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.localEulerAngles = new Vector3(0, 0, 0);
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    private void Clear()
    {
        if (contentParent != null)
            Destroy(contentParent);
        MapNodesUI.Clear();
        lineConnections.Clear();
    }

    private void GenerateMapNodeUI(MapNode mapNode)
    {
        GameObject mapNodeObject = Instantiate(mapNodeUIPrefab, contentParent.transform);
        MapNodeUI mapNodeUI = mapNodeObject.GetComponent<MapNodeUI>();
        MapNodeTypeSO mapNodeTypeData = GetMapNodeTypeData(mapNode.type);

        //TODO index of level to load
        int levelIndex = 0;
        mapNodeUI.SetUp(mapNode, mapNodeTypeData, levelIndex);
        mapNodeObject.transform.localPosition = CalculatePosition(mapNode.position);
        //mapNode.position = mapNodeObject.transform.localPosition;
        MapNodesUI.Add(mapNodeUI);
    }
    private Vector2 CalculatePosition(Vector2 postitionNormalized)
    {
        float newX = 9;// = math.remap(0f, 1f, -contentX * 3 / 8, +contentX * 3 / 8, postitionNormalized.x);
        float newY = 1;// = math.remap(0f, 1f, -contentY * 3 / 8, +contentY * 3 / 8, postitionNormalized.y);


        return new Vector2(newX, newY);
    }
    private void DrawLines()
    {
        foreach (var node in MapNodesUI)
        {
            foreach (var connection in node.mapNode.outNodesLocations)
                AddLineConnection(node, GetNode(connection));
        }
    }
    protected void AddLineConnection(MapNodeUI from, MapNodeUI to)
    {
        if (uiLinePrefab == null) return;

        var lineRenderer = Instantiate(uiLinePrefab, contentParent.transform);
        lineRenderer.transform.SetAsFirstSibling();
        var fromRT = from.transform as RectTransform;
        var toRT = to.transform as RectTransform;
        var fromPoint = fromRT.anchoredPosition +
                        (toRT.anchoredPosition - fromRT.anchoredPosition).normalized * offsetFromNodes;

        var toPoint = toRT.anchoredPosition +
                      (fromRT.anchoredPosition - toRT.anchoredPosition).normalized * offsetFromNodes;

        // drawing lines in local space:
        lineRenderer.transform.position = from.transform.position +
                                          (Vector3)(toRT.anchoredPosition - fromRT.anchoredPosition).normalized *
                                          offsetFromNodes;

        // line renderer with 2 points only does not handle transparency properly:
        var list = new List<Vector2>();
        for (var i = 0; i < linePointsCount; i++)
        {
            list.Add(Vector3.Lerp(Vector3.zero, toPoint - fromPoint +
                                                2 * (fromRT.anchoredPosition - toRT.anchoredPosition).normalized *
                                                offsetFromNodes, (float)i / (linePointsCount - 1)));
        }
        lineRenderer.Points = list.ToArray();
        lineConnections.Add(new LineConnection(null, lineRenderer, from, to));
    }
    protected MapNodeUI GetNode(Point p)
    {
        return MapNodesUI.FirstOrDefault(n => n.mapNode.locationOnMap.Equals(p));
    }
    public void SetAttainableNodes()
    {
        // first set all the nodes as unattainable/locked:
        foreach (var node in MapNodesUI)
            node.SetState(MapNodeUIStates.Locked);

        if (mapManager.currentMap.path.Count == 0)
        {
            // we have not started traveling on this map yet, set entire first layer as attainable:
            foreach (var node in MapNodesUI.Where(n => n.mapNode.locationOnMap.y == 0))
                node.SetState(MapNodeUIStates.Attainable);
        }
        else
        {
            // we have already started moving on this map, first highlight the path as visited:
            foreach (var point in mapManager.currentMap.path)
            {
                var mapNode = GetNode(point);
                if (mapNode != null)
                    mapNode.SetState(MapNodeUIStates.Visited);
            }

            var currentPoint = mapManager.currentMap.path[mapManager.currentMap.path.Count - 1];
            var currentNode = mapManager.currentMap.GetNode(currentPoint);

            // set all the nodes that we can travel to as attainable:
            foreach (var point in currentNode.outNodesLocations)
            {
                var mapNode = GetNode(point);
                if (mapNode != null)
                    mapNode.SetState(MapNodeUIStates.Attainable);
            }
        }
    }
    public void SetLineColors()
    {

        foreach (var connection in lineConnections)
            connection.SetColor(lineLockedColor);

        if (mapManager.currentMap.path.Count == 0)
        {
            foreach (var node in MapNodesUI.Where(n => n.mapNode.locationOnMap.y == 0))
            {
                var currentNodeZero = mapManager.currentMap.GetNode(node.mapNode.locationOnMap);
                foreach (var point in currentNodeZero.outNodesLocations)
                {
                    var lineConnection = lineConnections.FirstOrDefault(conn => conn.from.mapNode == currentNodeZero &&
                                                                                conn.to.mapNode.locationOnMap.Equals(point));
                    lineConnection?.SetColor(lineAttainableColor);
                }

            }
            return;
        }

        var currentPoint = mapManager.currentMap.path[mapManager.currentMap.path.Count - 1];
        var currentNode = mapManager.currentMap.GetNode(currentPoint);

        foreach (var point in currentNode.outNodesLocations)
        {
            var lineConnection = lineConnections.FirstOrDefault(conn => conn.from.mapNode == currentNode &&
                                                                        conn.to.mapNode.locationOnMap.Equals(point));
            lineConnection?.SetColor(lineAttainableColor);
        }

        if (mapManager.currentMap.path.Count <= 1) return;

        for (var i = 0; i < mapManager.currentMap.path.Count - 1; i++)
        {
            var current = mapManager.currentMap.path[i];
            var next = mapManager.currentMap.path[i + 1];
            var lineConnection = lineConnections.FirstOrDefault(conn => conn.@from.mapNode.locationOnMap.Equals(current) &&
                                                                        conn.to.mapNode.locationOnMap.Equals(next));

            lineConnection?.SetColor(lineVisitedColor);
        }
    }
    private MapNodeTypeSO GetMapNodeTypeData(MapNodeType type)
    {
        return mapConfigs.mapNodeTypes.FirstOrDefault(n => n.MapNodeType == type);
    }

}
