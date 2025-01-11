using UnityEngine;
using UnityEngine.UI;

public class CreditsInterface : MonoBehaviour
{
    [SerializeField] Button backToMenuBtn = null;

    [SerializeField] SoundOld buttonHover = null;
    [SerializeField] SoundOld buttonPressed = null;
    private void Awake()
    {
        backToMenuBtn.onClick.AddListener(() => BacktButtonOnClick());
    }

    void BacktButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        SceneLoader.Instance.LoadScene(SceneConstants.MenuScene);
    }

    public void ButtonOnHover()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonHover);

    }

}
