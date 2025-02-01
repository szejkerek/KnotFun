using PlaceHolders.Core;
using PlaceHolders.Audio;
using PlaceHolders.EventChannel;
using PlaceHolders.SceneTransitions;
using PlaceHolders.DataPersistence;
using TMPro;
using UnityEngine;

namespace PlaceHolders
{
    public class SystemTester : MonoBehaviour, IDataPersistence 
    {
        public TMP_Text coutner;
        public Sound soundSpaciua;
        public VoidEvent testEvent;
        public IntEvent testEvent2;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                CoreManager.AudioManager.Play(soundSpaciua);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                CoreManager.SceneLoader.LoadScene(1);
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                CoreManager.SceneLoader.LoadScene(2);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                testEvent.Raise();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                testEvent2.Raise(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                testEvent2.Raise(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                testEvent2.Raise(3);
            }

        }

        int counterInt = 0;
        public void IncrementCounter()
        {
            counterInt++;
            coutner.text = counterInt.ToString();
        }

        public void CaptureState(GameData GameData)
        {
            GameData.Clicks = counterInt;
        }

        public void RestoreState(GameData GameData)
        {
            counterInt = GameData.Clicks;
        }
    }
}
