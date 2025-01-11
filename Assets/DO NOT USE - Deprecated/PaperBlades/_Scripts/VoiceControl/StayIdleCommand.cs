using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice VoicePhrases/Stay Idle Command")]
public class StayIdleCommand : VoiceCommand
{
    public static Action<StayIdleCommand> OnIdleBigRecognized;
    public override void Execute()
    {
        if (Cooldown.IsOffCooldown())
        {
            OnIdleBigRecognized?.Invoke(this);
            Cooldown.ResetTimers();
        }
    }
}
