using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

namespace PlaceHolders.DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] bool disableDataPersistance = false;
        [SerializeField] bool InitializeDataIfNull = false;
        [SerializeField] bool overrideProfile = false;
        [SerializeField] string overridedProfile = "developer";

        [Header("Config")]
        [SerializeField] bool useEncription;
        [SerializeField] string encryptionKey;
        [SerializeField] string filename;
        
        ProfileFileUtility<GameData> fileUtility;
        GameData gameData;
        string selectedProfileId;

        private void OnEnable()
        {
            fileUtility = new ProfileFileUtility<GameData>(filename, useEncription, encryptionKey);


            if (disableDataPersistance)
                Debug.LogWarning("Data persistance is disabled!");

            if (overrideProfile)
            {
                selectedProfileId = overridedProfile;
                Debug.LogWarning($"Profile Id was replaced with: {overridedProfile}");
            }
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            if (disableDataPersistance)
                return;

            gameData = fileUtility.Load(selectedProfileId);

            if (!IsGameDataValid() && InitializeDataIfNull)
            {
                NewGame();
            }

            if (!IsGameDataValid())
            {
                Debug.Log("Not data was found. Create a New Game first!");
                return;
            }

            foreach (IDataPersistence obj in FindAllDataPersistencObjects())
            {
                obj.RestoreState(gameData);
            }
        }

        public void SaveGame()
        {
            if (disableDataPersistance)
                return;

            if (!IsGameDataValid())
            {
                return;
            }

            foreach (var obj in FindAllDataPersistencObjects())
            {
                obj.CaptureState(gameData);
            }

            gameData.lastUpdated = DateTime.Now.ToBinary();
            fileUtility.Save(gameData, selectedProfileId);
        }

        public void ReloadToNewProfile(string newProfile)
        {
            selectedProfileId = newProfile;
            LoadGame();
        }

        List<IDataPersistence> FindAllDataPersistencObjects()
        {
            IEnumerable<IDataPersistence> objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
            return new List<IDataPersistence>(objects);
        }

        public bool IsGameDataValid()
        {
            return gameData != null;
        }

        public Dictionary<string, GameData> GetAllProfileGameData()
        {
            return fileUtility.LoadAllProfilesData();
        }
    }
}