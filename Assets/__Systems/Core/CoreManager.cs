using UnityEngine;
using PlaceHolders.Audio;
using PlaceHolders.SceneTransitions;
using PlaceHolders.DataPersistence;

namespace PlaceHolders.Core
{
    public class CoreManager : MonoBehaviour
    {
        public static AudioManager AudioManager { get; private set; }
        public static SceneLoader SceneLoader { get; private set; }
        public static SceneTransitionManager SceneTransitionManager { get; private set; }
        public static DataPersistenceManager DataPersistenceManager { get; private set; }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            SceneTransitionManager = GetComponentInChildren<SceneTransitionManager>();
            DataPersistenceManager = GetComponentInChildren<DataPersistenceManager>();
            AudioManager = GetComponentInChildren<AudioManager>();
            SceneLoader = GetComponentInChildren<SceneLoader>();
            SceneTransitionManager = GetComponentInChildren<SceneTransitionManager>();
        }
    }

}
