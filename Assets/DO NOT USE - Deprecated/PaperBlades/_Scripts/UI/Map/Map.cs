using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Map
{
    public List<MapNode> mapNodes;
    public List<Point> path;
    public string configName;

    public Map()
    {
        this.mapNodes = null;
        this.path = null;
        this.configName = "";
    }

    public Map(string configName, List<MapNode> mapNodes, List<Point> path)
    {
        this.mapNodes = mapNodes;
        this.path = path;
        this.configName = configName;
    }

    public MapNode GetNode(Point point)
    {
        return mapNodes.FirstOrDefault(n => n.locationOnMap.Equals(point));
    }

    public void Save()
    {
        SaveManager<Map>.Save(this, configName + ".json");
    }
}
