using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice VoicePhrases/Normal Stance Command")]
public class StanceNormalCommand : VoiceCommand
{
    public static Action<StanceNormalCommand> OnNormalStanceRecognized;
    public override void Execute()
    {
        if (Cooldown.IsOffCooldown())
        {
            OnNormalStanceRecognized?.Invoke(this);
            Cooldown.ResetTimers();
        }
    }
}
