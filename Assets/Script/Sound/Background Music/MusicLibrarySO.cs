using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicLibrarySO", menuName = "Scriptable Objects/Sounds/MusicLibrarySO")]
public class MusicLibrarySO : ScriptableObject
{
    public AudioClip mainMenuMusics;
    public AudioClip homeMusics;
    public List<StageMusicSO> stageMusics;
    public AudioClip finalBossMusic;

    public StageMusicSO GetStageMusic(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageMusics.Count) return null;
        return stageMusics[stageIndex];
    }

    //public AudioClip GetRandomFinalBossMusic() => GetRandomClip(finalBossMusics);

    //private AudioClip GetRandomClip(AudioClip[] clips)
    //{
    //    if (clips == null || clips.Length == 0) return null;
    //    return clips[Random.Range(0, clips.Length)];
    //}
}
