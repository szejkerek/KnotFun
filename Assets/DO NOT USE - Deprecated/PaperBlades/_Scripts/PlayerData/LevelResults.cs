using System;
using System.Collections.Generic;

[Serializable]
public class LevelResults
{
    public bool isWin;
    public int colectedMoney;

    public int moneyOnWin;
    public int moneyOnLose;

    public List<Character> deadCharacters = new();
    public List<Character> newCharacters = new();

    public void Apply()
    {
        SavableDataManager.Instance.data.playerResources.AddMoney(CalculateMoney());

        foreach (Character character in deadCharacters)
        {
            SavableDataManager.Instance.data.team.KillCharacter(character);
        }

        foreach (Character character in newCharacters)
        {
            SavableDataManager.Instance.data.team.AddCharacter(character);
        }

        ClearData();
    }


    private int CalculateMoney()
    {
        if (isWin)
        {
            return moneyOnWin + colectedMoney;
        }
        return colectedMoney + moneyOnLose;
    }

    private void ClearData()
    {
        isWin = false;
        colectedMoney = 0;
        moneyOnWin = 0;
        moneyOnLose = 0;
        deadCharacters.Clear();
    }
}
