using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;
    private Coroutine musicTransitionCoroutine;

    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    public void PlayMusic(AudioClip newMusicClip, float targetVolume = 1f)
    {
        if (audioSource.clip == newMusicClip && audioSource.isPlaying)
            return;

        if (musicTransitionCoroutine != null)
            StopCoroutine(musicTransitionCoroutine);

        musicTransitionCoroutine = StartCoroutine(FadeToNewTrack(newMusicClip, targetVolume));
    }

    private IEnumerator FadeToNewTrack(AudioClip newClip, float targetVolume = 1f)
    {
        // Fade out
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    public void StopMusic()
    {
        if (musicTransitionCoroutine != null)
            StopCoroutine(musicTransitionCoroutine);
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
