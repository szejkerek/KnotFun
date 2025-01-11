using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


public enum SoundType
{
    Music,
    SFX
}
public class AudioManagerOld : Singleton<AudioManagerOld>
{
    [SerializeField] AudioMixerGroup masterMixer;
    [SerializeField] AudioMixerGroup sfxMixer;
    [SerializeField] AudioMixerGroup musicMixer;

    [SerializeField] AudioSource musicSource;
    [SerializeField] SoundOld menuAmbient;

    void Awake()
    {
        SetMixer(musicSource, SoundType.Music);
    }

    public void PlayOnTarget(GameObject target, SoundOld sound)
    {
        var sourceObj = target.AddComponent<AudioSource>();
        Play(sourceObj, sound, SoundType.SFX);
        Destroy(sourceObj, sound.Clip.length + 0.4f);
    }

    public void PlayAtPosition(Vector3 position, SoundOld sound)
    {
        GameObject gameObject = new GameObject(sound.name);
        var sourceObj = gameObject.AddComponent<AudioSource>();
        Play(sourceObj, sound, SoundType.SFX);

        Destroy(gameObject, sound.Clip.length + 0.4f);
    }

    public void PlayMusic(SoundOld sound)
    {
        Play(musicSource, sound, SoundType.Music);
    }

    public void Play(AudioSource source, SoundOld sound, SoundType type)
    {
        if (sound == null || sound.Clip == null)
        {
            Debug.LogWarning($"Sound of {source.gameObject.name} is null");
            return;
        }

        source.clip = sound.Clip;
        source.volume = sound.Volume;

        if (sound.PitchVariation > 0)
        {
            source.pitch = 1 + Random.Range(-sound.PitchVariation, sound.PitchVariation);
        }
        else
        {
            source.pitch = 1;
        }

        source.loop = sound.Loop;

        source.spatialBlend = sound.Settings3D.SpatialBlend ? 1f : 0f;
        source.minDistance = sound.Settings3D.MinDistance;
        source.maxDistance = sound.Settings3D.MaxDistance;

        SetMixer(source, type);

        source.Play();
    }

    public void PlayGlobal(SoundOld sound, SoundType type = SoundType.SFX)//, bool force)
    {
        if (sound == null || sound.Clip == null)
        {
            Debug.LogWarning("No sound");
            return;
        }

        //if (type == SoundType.Event)
        {
            //musicSource.Stop();
            PlayOnTarget(gameObject, sound);

            //StartCoroutine(FadeInMusic(sound, 1f));
        }
        //else if (type == SoundType.Music)
        {
            //if(musicSource.isPlaying)
            PlayMusic(sound);
        }
        //else
            PlayOnTarget(gameObject, sound);
    }

    private IEnumerator FadeInMusic(SoundOld sound, float duration)
    {
        float startVolume = 0.0f;
        musicSource.volume = startVolume;

        float currentTime = 0.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, sound.Volume, currentTime / duration);
            yield return null;
        }

        musicSource.volume = sound.Volume;
    }

    private void SetMixer(AudioSource source, SoundType type)
    {
        switch (type)
        {
            case SoundType.SFX:
                if (sfxMixer != null)
                    source.outputAudioMixerGroup = sfxMixer;
                break;
            case SoundType.Music:
                if (musicMixer != null)
                    source.outputAudioMixerGroup = musicMixer;
                break;
        }
    }

    public void SetVolume(float value)
    {
        value = Mathf.Clamp01(value) * 30 - 20;

        if (value <= -19)
            value = float.MinValue;

        if (masterMixer != null)
            masterMixer.audioMixer.SetFloat("MasterVolume", value);
    }
}
