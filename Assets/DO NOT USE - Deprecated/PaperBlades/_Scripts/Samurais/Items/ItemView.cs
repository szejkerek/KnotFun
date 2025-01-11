using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text displayName;
    [SerializeField] TMP_Text costText;
    UpgradeableComponent UpgradeableComponent;
    Item currentItem;
    private void Awake()
    {
        UpgradeableComponent = GetComponentInChildren<UpgradeableComponent>();
        UpgradeableComponent.OnMaxedLevel += TryEvolve;
        UpgradeableComponent.OnUpgrade += SetupCost;
    }

    private void OnDestroy()
    {
        UpgradeableComponent.OnMaxedLevel -= TryEvolve;
        UpgradeableComponent.OnUpgrade += SetupCost;
    }

    private void TryEvolve()
    {
        Item item = currentItem.TryGetNextItem();
        if (item != null)
        {

            Character currentCharacter = FindObjectOfType<TeamManagementInterface>().CharacterPanel.CurrentCharacter;
            if (currentCharacter != null)
            {
                if (currentCharacter.Weapon == currentItem)
                {
                    currentCharacter.Weapon = item;
                }
                if (currentCharacter.Armor == currentItem)
                {
                    currentCharacter.Armor = item;
                }
                if (currentCharacter.Skill == currentItem)
                {
                    currentCharacter.Skill = item;
                }

                Init(item);
            }
        }
        else
        {
            UpgradeableComponent.BlockUpgrades();
        }
    }

    public void Init(Item item)
    {
        currentItem = item;
        SetupDisplay(item);
        SetupUpgradable(item);
        SetupCost();
    }

    private void SetupCost()
    {
        costText.text = currentItem.CostFunction().ToString();
    }

    void SetupUpgradable(Item upgradebleItem)
    {
        UpgradeableComponent?.Init(upgradebleItem);
    }

    void SetupDisplay(IDisplayable displayable)
    {
        displayName.text = displayable.DisplayName;

        Color iconColor = icon.color;
        iconColor.a = 0;
        icon.color = iconColor;

        var iconReference = new AssetReference(displayable.DisplayIconGuid);
        iconReference.LoadAssetAsync<Sprite>().Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                icon.sprite = handle.Result;
                iconColor.a = 1;
                icon.color = iconColor;
            }
            else
            {
                Debug.LogError("Failed to load icon sprite.");
            }
        };
    }

    public void RestartDisplay()
    {
        icon.sprite = null;
        displayName.text = "";
    }
}
