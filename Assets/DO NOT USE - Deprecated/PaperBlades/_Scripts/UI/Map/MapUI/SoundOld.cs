using UnityEngine;


    public class SoundOld : ScriptableObject
    {
        public SoundType SoundType => soundType;
        [SerializeField] SoundType soundType = SoundType.SFX;

        [field: SerializeField] public AudioClip Clip { private set; get; }
        public float Volume => volume;
        [SerializeField, Range(0, 1)] float volume = 1;

        public float PitchVariation => pitchVariation;
        [SerializeField, Range(0, 3)] float pitchVariation = 0;
        public bool Loop => loop;
        [SerializeField] bool loop = false;
        [field: SerializeField] public Settings3D Settings3D { private set; get; }


    }
