using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelManager : MonoBehaviour
{
    [SerializeField] Button teamManagementBtn;
    [SerializeField] Button returnBtn;

    [SerializeField] SoundOld buttonHover = null;
    [SerializeField] SoundOld buttonPressed = null;

    private void Awake()
    {
        teamManagementBtn.onClick.AddListener(() => ManagementButtonOnClick());
        returnBtn.onClick.AddListener(() => MenuButtonOnClick());
    }

    void ManagementButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        SceneLoader.Instance.LoadScene(SceneConstants.ManagementScene);
    }
    void MenuButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        SceneLoader.Instance.LoadScene(SceneConstants.MenuScene);
    }

    public void ButtonOnHover()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonHover);

    }

}
