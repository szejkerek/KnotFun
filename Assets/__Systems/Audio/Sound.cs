using UnityEngine;


namespace PlaceHolders.Audio
{
    [CreateAssetMenu(fileName = "New Sound", menuName = "Audio/Sound Clip")]
    public class Sound : ScriptableObject
    {
        public float Lenght => GetDeltaLength();
        public SoundType SoundType => soundType;

        [SerializeField] AudioClip clip;
        [SerializeField] SoundType soundType = SoundType.SFX;
        [SerializeField, Range(0, 1)] float volume = 1;
        [SerializeField, MinMaxSlider(0, 3)] Vector2 pitch = new Vector2(1, 1);
        [SerializeField] bool loop = false;
        [field: SerializeField] public Settings3D Settings3D { private set; get; } //TODO: Extend Settings3D, move spacialblend bool out, write editor to hide it


        public float GetVolume(CustomAudioSettings audioSettings)
        {
            if (audioSettings.AudioMuted)
                return 0;

            return soundType switch
            {
                SoundType.SFX => volume * audioSettings.SfxVolume,
                SoundType.Music => audioSettings.MusicMuted ? 0 : volume * audioSettings.MusicVolume,
                _ => 1,
            };
        }

        float GetDeltaLength()
        {
            return clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale) + 0.5f;
        }

        public void Play(AudioSource source, CustomAudioSettings settings)
        {
            if (clip == null)
            {
                Debug.LogWarning($"There is no sound clip to play on {name}.");
                return;
            }

            source.clip = clip;
            source.loop = loop;
            source.volume = GetVolume(settings);
            source.minDistance = Settings3D.MinDistance;
            source.maxDistance = Settings3D.MaxDistance;
            source.spatialBlend = Settings3D.SpatialBlend ? 1f : 0f;
            source.pitch = pitch.ValueBetween();

            source.Play();
        }
    }

}