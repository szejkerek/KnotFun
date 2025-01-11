using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SamuraiNames : ScriptableObject
{
    public List<string> names;
    public List<string> surnames;

    public string GetRandomName()
    {
        string randomName = names.SelectRandomElement();
        string randomSurname = surnames.SelectRandomElement();

        return randomName + " " + randomSurname;
    }
}
