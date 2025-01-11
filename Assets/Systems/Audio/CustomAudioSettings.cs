using UnityEngine;

namespace PlaceHolders.Audio
{
    [System.Serializable]
    public class CustomAudioSettings 
    {
        public bool AudioMuted = false;
        public bool MusicMuted = false;
        [Range(0f, 1f)] public float SfxVolume = 1f;
        [Range(0f, 1f)] public float MusicVolume = 1f;
    }
}
