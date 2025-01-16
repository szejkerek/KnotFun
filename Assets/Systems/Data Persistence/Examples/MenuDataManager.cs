using PlaceHolders.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PlaceHolders.DataPersistence
{
    public class MenuDataManager : MonoBehaviour
    {
        [SerializeField] Button NewGameButton;
        [SerializeField] Button ContinueButton;
        [SerializeField] Button BackButton;
        [SerializeField] Button LoadGameButton;
        [SerializeField] GameObject MainMenu;
        [SerializeField] GameObject SaveSlotsMenu;

        SaveSlot[] SaveSlots;
        void Awake()
        {
            ActivateMenu(true);
            NewGameButton.onClick.AddListener(OnNewGameClick);
            ContinueButton.onClick.AddListener(OnContinueClick);
            BackButton.onClick.AddListener(() => ActivateMenu(true));
            LoadGameButton.onClick.AddListener(() => ActivateMenu(false));
        }

        private void Start()
        {
            if (!CoreManager.DataPersistenceManager.IsGameDataValid())
            {
                ContinueButton.interactable = false;
            }
            SaveSlots = SaveSlotsMenu.GetComponentsInChildren<SaveSlot>();
            ActivateSaveSlots();
        }
        


        void ActivateMenu(bool state)
        {
            MainMenu.gameObject.SetActive(state);
            SaveSlotsMenu.gameObject.SetActive(!state);
        }

        void OnNewGameClick()
        {
            CoreManager.DataPersistenceManager.NewGame();
            CoreManager.SceneLoader.LoadScene(5);
            DisableAllButtons();
        }

        void OnContinueClick()
        {
            CoreManager.SceneLoader.LoadScene(5);
            DisableAllButtons();
        }

        void DisableAllButtons()
        {
            NewGameButton.interactable = false;
            ContinueButton.interactable = false;
            BackButton.interactable = false;
            LoadGameButton.interactable = false;    

            foreach(SaveSlot saveSlot in SaveSlots)
            {
                saveSlot.GetComponent<Button>().interactable = false;
                saveSlot.OnClicked -= OnSaveSlotClicked;
            }
        }

        public void ActivateSaveSlots()
        {
            var profiles = CoreManager.DataPersistenceManager.GetAllProfileGameData();
            foreach (SaveSlot saveSlot in SaveSlots)
            {
                profiles.TryGetValue(saveSlot.ProfileId, out GameData data);
                saveSlot.SetData(data);
                saveSlot.OnClicked += OnSaveSlotClicked;
            }
        }

        private void OnSaveSlotClicked(string profileId)
        {
            DisableAllButtons();
            CoreManager.DataPersistenceManager.ReloadToNewProfile(profileId);
            CoreManager.SceneLoader.LoadScene(5);
        }
    }
}