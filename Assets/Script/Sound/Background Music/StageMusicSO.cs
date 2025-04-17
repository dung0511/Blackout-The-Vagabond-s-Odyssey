using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageMusicSO", menuName = "Scriptable Objects/Sounds/StageMusicSO")]
public class StageMusicSO : ScriptableObject
{
    public List<AudioClip> dungeonMusics;
    public List<AudioClip> eliteMusics;
    public List<AudioClip> bossMusics;

    public AudioClip GetRandomDungeonMusic() => GetRandomClip(dungeonMusics);
    public AudioClip GetRandomEliteMusic() => GetRandomClip(eliteMusics);
    public AudioClip GetRandomBossMusic() => GetRandomClip(bossMusics);

    private AudioClip GetRandomClip(List<AudioClip> clips)
    {
        if (clips == null || clips.Count == 0) return null;
        return clips[Random.Range(0, clips.Count)];
    }
}
