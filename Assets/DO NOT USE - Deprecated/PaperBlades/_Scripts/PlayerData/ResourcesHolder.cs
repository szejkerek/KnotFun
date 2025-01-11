using System;

[Serializable]
public class ResourcesHolder
{
    public static Action<ResourcesHolder> OnResourcesUpdated;
    public int Money;

    public void AddMoney(int money)
    {
        Money += money;
        InfoTextManager.Instance.AddInformation($"You gained {money} money.", InfoLenght.Long);
        OnResourcesUpdated?.Invoke(this);
    }

    public bool TryRemoveMoney(int money)
    {
        money = Math.Abs(money);
        if (Money - money < 0)
            return false;

        Money -= money;
        InfoTextManager.Instance.AddInformation($"You lost {money} money.", InfoLenght.Long);
        OnResourcesUpdated?.Invoke(this);
        return true;
    }
}
