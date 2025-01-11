
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapPlayerTracker : Singleton<MapPlayerTracker>
{
    public Action<int> OnPopupChooose;
    public bool lockAfterSelecting = false;
    public MapManager mapManager;
    public MapDrawerUI view;
    public bool locked;
    public PopupAllEventsSO events;

    [SerializeField] SoundOld armory = null;
    [SerializeField] SoundOld boss = null;
    [SerializeField] SoundOld exp = null;
    [SerializeField] SoundOld forge = null;
    [SerializeField] SoundOld temple = null;
    [SerializeField] SoundOld rerol = null;

    public PassiveAllEffectSO passiveEffects;
    public List<AssetReferenceCharacterSO> possibleCharacters;
    public void SendPlayerToNode(MapNodeUI mapNode)
    {
        if (locked) return;
        locked = lockAfterSelecting;
        mapManager.currentMap.path.Add(mapNode.mapNode.locationOnMap);
        SavableDataManager.Instance.data.map = mapManager.currentMap;
        SavableDataManager.Instance.Save();

        switch (mapNode.mapNode.type)
        {
            case MapNodeType.Arena:
                int sceneIndex = SceneConstants.Level_1_2;
                if (mapManager.currentMap.path.Count > 1)
                {
                    sceneIndex = UnityEngine.Random.Range(SceneConstants.Level_1, SceneConstants.Level_1_3 + 1);
                }

                SceneLoader.Instance.LoadScene(sceneIndex);
                break;
            case MapNodeType.Armory:
                PopupController.Instance.PopupPanel.ChooseModal(GetNewRandomCharacters(), TryAddNewCharacter, "Choose new ally", "Our elite squad has the opportunity to recruit a new, exceptional ally. Each candidate brings unique skills and experience that can change the course of our missions. Carefully consider their abilities and stats to choose the warrior who will best complement our team.");
                break;
            case MapNodeType.Boss:
                PlaySound(boss);
                PopupController.Instance.PopupPanel.ShowAsEvent(events.BossEvents.SelectRandomElement());
                break;
            case MapNodeType.Experience:
                PlaySound(exp);
                PopupController.Instance.PopupPanel.ShowAsEvent(events.ExperienceEvents.SelectRandomElement());
                break;
            case MapNodeType.Forge:
                PlaySound(forge);
                PopupController.Instance.PopupPanel.ShowAsEvent(events.ForgeEvents.SelectRandomElement());
                break;
            case MapNodeType.Temple:
                PlaySound(temple);
                PopupController.Instance.PopupPanel.ShowAsEvent(events.TempleEvents.SelectRandomElement());
                break;
            case MapNodeType.WeaponReroll:

                List<Character> choices2 = SavableDataManager.Instance.data.team.TeamMembers.SelectRandomElements(3);
                if (choices2.Count < 3) choices2.Add(SavableDataManager.Instance.data.team.General);

                PassiveEffectSO passiveEffectRandom = passiveEffects.allPassiveEffectSO.SelectRandomElement();
                string passiveName = passiveEffectRandom.GetName();
                string header = String.Format("Choose ally to get <b>{0}</b> passive", passiveName);
                string content = String.Format("Now we can give <b>{0}</b> passive to one of our party memebers", passiveName);

                PopupController.Instance.PopupPanel.ChooseModal(passiveEffectRandom, choices2, TryAddPasiveToCharacter, header, content);
                break;
            default:
                throw new NotImplementedException();
        }

        StartCoroutine(UpdateMap());
    }

    public IEnumerator UpdateMap()
    {
        yield return new WaitForSeconds(1f);
        view.SetAttainableNodes();
        view.SetLineColors();
    }

    private List<Character> GetNewRandomCharacters()
    {
        List<Character> characters = new();
        for (int i = 0; i < 3; i++)
        {
            characters.Add(new Character(possibleCharacters.SelectRandomElement().AssetGUID));
        }
        return characters;

    }

    private void TryAddNewCharacter(Character character)
    {
        if (character == null)
        {
            Debug.Log("Couldnt get Character from modal.");
            return;
        }
        SavableDataManager.Instance.data.team.AddCharacter(character);
    }

    private void TryAddPasiveToCharacter(Character character, PassiveEffectSO newPasive)
    {
        if (character == null)
        {
            Debug.Log("Couldnt get Character from modal.");
            return;
        }
        character.PassiveEffects.Add(newPasive);
    }


    public void PlaySound(SoundOld sound)
    {
        AudioManagerOld.Instance.PlayGlobal(sound);
    }
}
