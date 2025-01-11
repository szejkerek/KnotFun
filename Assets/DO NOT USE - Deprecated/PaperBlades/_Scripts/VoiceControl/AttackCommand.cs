using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice VoicePhrases/Attack Command")]
public class AttackCommand : VoiceCommand
{
    public static Action<AttackCommand> OnAttackRecognized;
    public override void Execute()
    {
        if (Cooldown.IsOffCooldown())
        {
            OnAttackRecognized?.Invoke(this);
            Cooldown.ResetTimers();
        }
    }
}
