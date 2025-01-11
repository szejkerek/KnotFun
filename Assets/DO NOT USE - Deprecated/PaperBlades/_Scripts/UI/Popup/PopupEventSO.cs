using UnityEngine;

[CreateAssetMenu]
public class PopupEventSO : ScriptableObject
{
    public string Header;
    [TextAreaAttribute] public string Content;
    public Sprite DisplayImage;
    public string answerOne;
    public string answerTwo;
    public PopupEventEffect OnAccept;
    public PopupEventEffect OnDecline;
}

[System.Serializable]
public class PopupEventEffect
{
    public bool AddMoney = true;
    public int Money;
    [Space]
    public bool AddPassive = false;
    public PassiveEffectSO passiveEffect;


    public void Apply()
    {


        if (AddMoney)
        {
            if (Money < 0)
            {
                SavableDataManager.Instance.data.playerResources.TryRemoveMoney(Money);
            }
            else
            {
                SavableDataManager.Instance.data.playerResources.AddMoney(Money);
            }
        }


        if (passiveEffect != null)
        {
            SavableDataManager.Instance.data.team.TeamMembers.SelectRandomElement().characterData.PassiveEffects.Add(passiveEffect);
        }
    }
}