using System.Collections;
using UnityEngine;
using DG.Tweening;

    public class AudioManager : MonoBehaviour
    {
        public CustomAudioSettings audioSettings;
        [Header("SFX")]
        [SerializeField] AudioSource sfxSource;

        [Header("Music")]
        [SerializeField] AudioSource musicSource;
        [SerializeField, Range(0f, 10f)] float musicFadeInTime = 8f;
        [SerializeField, Range(0f, 10f)] float musicFadeOutTime = 4f;
        
        Coroutine musicCorutine;

        void ChangeMusic(AudioSource source, Sound musicClip)
        {
            if (musicCorutine != null)
            {
                StopCoroutine(musicCorutine);
                musicCorutine = null;
                musicSource.volume = 0;
                musicSource.pitch = 0;
            }

            musicCorutine = StartCoroutine(FadeOutAndChangeMusic(musicClip));
        }

        private IEnumerator FadeOutAndChangeMusic(Sound musicClip)
        {
            if (musicSource.isPlaying)
            {
                yield return musicSource.DOFade(0f, musicFadeOutTime).WaitForCompletion();
                musicSource.volume = 0;
            }

            musicClip.Play(musicSource, audioSettings);
            musicSource.volume = 0;
            yield return musicSource.DOFade(musicClip.GetVolume(audioSettings), musicFadeInTime).WaitForCompletion();
            musicCorutine = null;
        }


        public void PlayOnTarget(GameObject target, Sound sound)
        {
            var sourceObj = target.AddComponent<AudioSource>();
            sound.Play(sourceObj, audioSettings);
            Destroy(sourceObj, sound.Lenght);
        }

        public void PlayAtPosition(Vector3 position, Sound sound)
        {
            GameObject gameObject = new GameObject($"One shot {sound.name} clip.");
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();

            if (!sound.Settings3D.SpatialBlend)
                Debug.LogWarning($"Sound {sound.name} is not spacial but was played in world space!");

            gameObject.transform.position = position;
            sound.Play(audioSource, audioSettings);
            Destroy(gameObject, sound.Lenght);
        }
        public void Play(Sound sound)
        {
            switch (sound.SoundType)
            {
                case SoundType.SFX:
                    sound.Play(sfxSource, audioSettings);
                    break;
                case SoundType.Music:
                    ChangeMusic(musicSource, sound);
                    break;
            }
        }
    }

