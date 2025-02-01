using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
namespace PlaceHolders
{
    public class LocalizationTestScript : MonoBehaviour
    {
        [SerializeField] private LocalizedString localizedString;
        [SerializeField] private TextMeshProUGUI text;

        private int score;

        private void OnEnable()
        {
            localizedString.Arguments = new object[] {score};
            localizedString.StringChanged += UpdateText;
        }
        private void OnDisable()
        {
            localizedString.StringChanged -= UpdateText;
        }

        private void UpdateText(string value)
        {
            text.text = value;
        }

        public void IncreaseScore()
        {
            score++;
            localizedString.Arguments[0] = score;
            localizedString.RefreshString();
        }
    }
}


