using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice VoicePhrases/Attack Small Command")]
public class AttackSmallCommand : VoiceCommand
{
    public static Action<AttackSmallCommand> OnAttackSmallRecognized;
    public override void Execute()
    {
        if (Cooldown.IsOffCooldown())
        {
            OnAttackSmallRecognized?.Invoke(this);
            Cooldown.ResetTimers();
        }
    }
}
