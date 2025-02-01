using PlaceHolders.Core;
using PlaceHolders.DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapsTest : MonoBehaviour, IDataPersistence
{
    int clickCount;
    public TMP_Text clickText;
    public Button ClickButton;
    public Button MenuButton;
    public Button NextSceneButton;

    private void Awake()
    {
        ClickButton.onClick.AddListener(() => { clickCount++; clickText.text = clickCount.ToString(); });
        MenuButton.onClick.AddListener(OnLoadMenu);
        NextSceneButton.onClick.AddListener(LoadOtherTestScene);
    }

    private void LoadOtherTestScene()
    {
        CoreManager.SceneLoader.LoadScene(5);
    }

    private void OnLoadMenu()
    {
        CoreManager.SceneLoader.LoadScene(4);
    }

    public void CaptureState(GameData GameData)
    {
        GameData.Taps = clickCount;
    }

    public void RestoreState(GameData GameData)
    {
        clickCount = GameData.Taps;
        clickText.text = clickCount.ToString();
    }
}
