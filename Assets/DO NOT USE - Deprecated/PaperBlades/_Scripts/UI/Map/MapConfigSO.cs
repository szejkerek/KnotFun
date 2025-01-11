using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMapConfig", menuName = "Map/MapConfig")]
public class MapConfigSO : ScriptableObject
{
    public int mapGridWidth;

    public int extraPaths;

    public int numOfStartingNodes;

    public int numOfPreBossNodes;

    public List<MapNodeTypeSO> mapNodeTypes;

    public List<MapLayer> mapLayers;


}
