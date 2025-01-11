using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int configIndex;
    public List<MapConfigSO> allMapConfigs;
    public MapDrawerUI mapUI;
    public Map currentMap { get; private set; }

    private void Start()
    {
        //SavableDataManager.Instance.Load();
        currentMap = SavableDataManager.Instance.data.map;


        if (currentMap == null || currentMap.mapNodes == null || currentMap.mapNodes.Count == 0)
        {
            GenerateNewMap();
        }
        else
        {
            mapUI.ShowMap(currentMap, allMapConfigs[configIndex]);
        }

    }

    public void GenerateNewMap()
    {
        currentMap = MapGenerator.CreateMap(allMapConfigs[configIndex]);
        mapUI.ShowMap(currentMap, allMapConfigs[configIndex]);
        SavableDataManager.Instance.data.map = currentMap;
        SavableDataManager.Instance.Save();
    }


}
