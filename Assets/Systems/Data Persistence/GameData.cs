using UnityEngine;

namespace PlaceHolders.DataPersistence
{
    [System.Serializable]
    public class GameData
    {
        public long lastUpdated;
        public int Clicks;
        public int Taps;

        //Remember to initialize fields here as they would be in the NewGame state!
        public GameData()
        {
            //Debug.Log("Constructing game data.");
            Clicks = 0;
            Taps = 0;
        }
    }
}

