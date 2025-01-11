using System.Collections.Generic;
using UnityEngine;

public abstract class VoiceCommand : ScriptableObject
{
    [field: SerializeField] public string DisplayPhrase { get; private set; }
    [field: SerializeField] public List<string> VoicePhrases { get; private set; }
    public Cooldown Cooldown = new Cooldown();
    public abstract void Execute();
}
