using Unity.Cinemachine;
using UnityEngine;

public class CinemachineInit : MonoBehaviour
{
    void Start()
    {
        var vcam = GetComponent<CinemachineCamera>();
        var player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            vcam.Follow = player.transform;
            vcam.LookAt = player.transform;
        }
        else
        {
            Debug.LogWarning("No player found for Cinemachine camera");
        }
    }
}
