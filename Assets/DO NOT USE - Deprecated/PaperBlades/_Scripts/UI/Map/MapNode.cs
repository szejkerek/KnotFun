using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MapNode
{
    public Vector2 position;

    public Point locationOnMap;

    public List<Point> outNodesLocations = new List<Point>();

    public List<Point> inNodesLocations = new List<Point>();

    public MapNodeType type;

    public MapNode(Point locationOnMap, MapNodeType type)
    {
        this.locationOnMap = locationOnMap;
        this.type = type;
    }

    public void AddOutNode(Point outNodesLocation)
    {
        if (outNodesLocations.Any(element => element.Equals(outNodesLocation)))
            return;

        outNodesLocations.Add(outNodesLocation);
    }

    public void AddInNode(Point inNodesLocation)
    {
        if (inNodesLocations.Any(element => element.Equals(inNodesLocation)))
            return;

        inNodesLocations.Add(inNodesLocation);
    }

    public void RemoveOutNode(Point p)
    {
        outNodesLocations.RemoveAll(element => element.Equals(p));
    }
    public void RemoveInNode(Point p)
    {
        inNodesLocations.RemoveAll(element => element.Equals(p));
    }



    public bool HasNoConnections()
    {
        return inNodesLocations.Count == 0 && outNodesLocations.Count == 0;
    }


}
