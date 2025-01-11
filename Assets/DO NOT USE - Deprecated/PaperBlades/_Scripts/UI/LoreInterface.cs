using UnityEngine;
using UnityEngine.UI;

public class LoreInterface : MonoBehaviour
{
    [SerializeField] Button startBtn = null;
    //[SerializeField] Button skipBtn = null;
    [SerializeField] Button quitBtn = null;

    [SerializeField] SoundOld buttonPressed = null;
    [SerializeField] SoundOld buttonHover = null;

    private void Awake()
    {
        startBtn.onClick.AddListener(() => LevelSceneButtonOnClick());
        //skipBtn.onClick.AddListener(() => SceneLoader.Instance.LoadScene(SceneConstants.ChooseLevelScene));
        quitBtn.onClick.AddListener(() => MenuSceneButtonOnClick());
    }

    void LevelSceneButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        SceneLoader.Instance.LoadScene(SceneConstants.GeneralDesign);
    }

    void MenuSceneButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
        SceneLoader.Instance.LoadScene(SceneConstants.MenuScene);
    }

    public void ButtonOnHover()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonHover);

    }
}
