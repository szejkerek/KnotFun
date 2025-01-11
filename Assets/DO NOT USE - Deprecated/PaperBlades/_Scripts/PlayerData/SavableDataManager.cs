using UnityEngine;
//
public class SavableDataManager : Singleton<SavableDataManager>, ISavable
{
    public SaveableData data = new SaveableData();
    [SerializeField] TeamSO team;
    void Awake()
    {
        RestartGame();
    }

    public void RestartGame(bool toMainMenu = false)
    {
        data = new SaveableData();
        data.team = new Team(team);
        if (toMainMenu)
        {
            SceneLoader.Instance.LoadScene(SceneConstants.MenuScene);
        }
    }

    #region Save Logic
    public string SavedFilename => "data.dat";

    public void Load()
    {
        data = SaveManager<SaveableData>.Load(SavedFilename);
        Save();
    }

    public void Save()
    {
        SaveManager<SaveableData>.Save(data, SavedFilename);
    }
    #endregion
}
