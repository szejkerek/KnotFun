using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice VoicePhrases/March Wander Command")]
public class MarchWanderCommand : VoiceCommand
{
    public static Action<MarchWanderCommand> onWanderRecognized;
    public override void Execute()
    {
        if (Cooldown.IsOffCooldown())
        {
            onWanderRecognized?.Invoke(this);
            Cooldown.ResetTimers();
        }
    }
}
