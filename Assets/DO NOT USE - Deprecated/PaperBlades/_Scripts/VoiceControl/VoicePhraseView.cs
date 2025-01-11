using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoicePhraseView : MonoBehaviour
{
    public TMP_Text text;
    public Slider slider;
    public GameObject readyBackgorund;
    public GameObject notreadyBackgorund;
    VoiceCommand command;
    public void Init(VoiceCommand command)
    {
        this.command = command;
        this.text.text = command.DisplayPhrase;
        command.Cooldown.ResetTimers(setBehind: true);
        slider.value = 1;
        notreadyBackgorund.SetActive(false);
        readyBackgorund.SetActive(true);
    }

    private void Update()
    {
        float progressRatio = command.Cooldown.ReadyToUseRatio();

        if (progressRatio == 1)
        {
            notreadyBackgorund.SetActive(false);
            readyBackgorund.SetActive(true);
            slider.gameObject.SetActive(false);
        }
        else
        {
            notreadyBackgorund.SetActive(true);
            readyBackgorund.SetActive(false);
            slider.gameObject.SetActive(true);

        }

        slider.value = command.Cooldown.ReadyToUseRatio();
    }
}
