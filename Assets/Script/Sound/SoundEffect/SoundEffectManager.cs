using System.Collections;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    [SerializeField] private SoundEffect soundFXObject;
    [SerializeField] private SoundEffectPool soundEffectPool;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Play the sound effect
    /// </summary>
    public void PlaySoundEffect(SoundEffectSO soundEffect)
    {
        // Play sound using a sound gameobject and component from the object pool
        //SoundEffect sound = Instantiate(soundFXObject, Vector3.zero, Quaternion.identity);
        SoundEffect sound = soundEffectPool.GetSoundEffect();
        sound.gameObject.SetActive(false);
        sound.SetSound(soundEffect);
        sound.gameObject.SetActive(true);


        StartCoroutine(DisableSound(sound, soundEffect.soundEffectClip.length));

    }


    /// <summary>
    /// Disable sound effect object after it has played thus returning it to the object pool
    /// </summary>
    private IEnumerator DisableSound(SoundEffect sound, float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);
        soundEffectPool.ReturnToPool(sound);
    }






}
