using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice VoicePhrases/Attack Normal Command")]
public class NormalAttackCommand : VoiceCommand
{
    public static Action<NormalAttackCommand> OnAttackNormalRecognized;
    public override void Execute()
    {
        if (Cooldown.IsOffCooldown())
        {
            OnAttackNormalRecognized?.Invoke(this);
            Cooldown.ResetTimers();
        }
    }
}
