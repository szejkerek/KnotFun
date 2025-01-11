using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Voice VoicePhrases/Follow Support Command")]
public class FollowSupportCommand : VoiceCommand
{
    public static Action<FollowSupportCommand> onFollowRecognized;
    public override void Execute()
    {
        if (Cooldown.IsOffCooldown())
        {
            onFollowRecognized?.Invoke(this);
            Cooldown.ResetTimers();
        }
    }
}
