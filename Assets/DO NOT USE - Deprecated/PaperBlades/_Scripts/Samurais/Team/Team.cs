using System;
using System.Collections.Generic;

[Serializable]
public class Team
{
    public Character General;
    public List<Character> TeamMembers;

    public Team(TeamSO team)
    {
        General = new Character(team.General.AssetGUID);
        TeamMembers = new List<Character>();

        foreach (var member in team.TeamMembers)
        {
            TeamMembers.Add(new Character(member.AssetGUID));
        }
    }

    public Team(Character General, List<Character> TeamMembers)
    {
        this.General = General;
        this.TeamMembers = TeamMembers;
    }

    public void KillCharacter(Character character)
    {
        TeamMembers.Remove(character);
    }

    public void AddCharacter(Character character)
    {
        TeamMembers.Add(character);
    }
}