using System.Collections;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private float delaySpawn = 0.5f;
    [SerializeField] private float delayBoss = 0.2f;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject spawnEffect;
    private Collider2D trigger;

    void Awake()
    {
        trigger = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            trigger.enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            SpawnBoss();

            BossHealthBarController.Instance.Show();
            GameManager.Instance.PlayCurrentStageBossMusic();
        }
        
    }

    private void SpawnBoss()
    {
        if(spawnEffect)
        {
            StartCoroutine(DelaySpawn());
        } else {
            StartCoroutine(DelayBoss());
        }
        
    }
    private GameObject anime;
    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(delaySpawn);
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        anime = Instantiate(spawnEffect, spawnPos.position + new Vector3(0,-0.25f,0), Quaternion.identity);
        StartCoroutine(DelayBoss());
    }

    private IEnumerator DelayBoss()
    {
        yield return new WaitForSeconds(delayBoss);
        var bossIndex = Random.Range(0, BossManager.Instance.bossList.Count);
        Instantiate(BossManager.Instance.bossList[bossIndex], spawnPos.position, Quaternion.identity);
        Destroy(anime);
        Destroy(gameObject);
    }
}
