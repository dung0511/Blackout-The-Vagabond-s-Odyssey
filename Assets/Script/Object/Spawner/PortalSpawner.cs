using System;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab;

    public void OnBossKilled()
    {
        Instantiate(portalPrefab, this.transform.position, Quaternion.identity);
    }
}
