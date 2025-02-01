using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlaceHolders.DataPersistence
{
    public class SaveSlot : MonoBehaviour
    {
        public Action<string> OnClicked;
        [field: SerializeField] public string ProfileId { get; private set; }
        [SerializeField] GameObject hasDataObject;
        [SerializeField] GameObject noDataObject;

        [SerializeField] TMP_Text ClicksTapsText;

        void Awake() => GetComponent<Button>().onClick.AddListener(() => OnClicked.Invoke(ProfileId));

        public void SetData(GameData data)
        {
            if(data == null)
            {
                hasDataObject.SetActive(false);
                noDataObject.SetActive(true);
            }
            else
            {
                hasDataObject.SetActive(true);
                noDataObject.SetActive(false);

                ClicksTapsText.text = $"Clicks: {data.Clicks}, Taps: {data.Taps}";
            }
        }
    }
}