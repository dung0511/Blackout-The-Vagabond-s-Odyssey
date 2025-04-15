using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPool : MonoBehaviour
{
    [SerializeField] private SoundEffect soundEffectPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<SoundEffect> pool = new Queue<SoundEffect>();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            SoundEffect sfx = CreateNewInstance();
            pool.Enqueue(sfx);
        }
    }

    private SoundEffect CreateNewInstance()
    {
        SoundEffect sfx = Instantiate(soundEffectPrefab, transform);
        sfx.gameObject.SetActive(false);
        return sfx;
    }

    public SoundEffect GetSoundEffect()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }

        // Pool hết, tạo thêm
        return CreateNewInstance();
    }

    public void ReturnToPool(SoundEffect sfx)
    {
        sfx.gameObject.SetActive(false);
        pool.Enqueue(sfx);
    }
}

