using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteTrigger : MonoBehaviour
{
    public GameObject spawnAnimation;
    [SerializeField] private float delaySpawn = 0.5f;
    [HideInInspector] public BoxRoom eliteRoom;
    [HideInInspector] public List<GameObject> barriers;
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isTriggered)
        {
            isTriggered = true;
            foreach(var barrier in barriers) //block entrances
            {
                //animation 
                barrier.SetActive(true);
            }

            GameManager.Instance.PlayCurrentStageEliteMusic();

            EnableElite(); //appear animation then re enable elite
        }
    }

    private void EnableElite()
    {
        var anim = Instantiate(spawnAnimation, eliteRoom.eliteReference.transform.position, Quaternion.identity);
        StartCoroutine(WaitSpawnAnimation(delaySpawn));
        Destroy(anim, delaySpawn+0.5f);
        Destroy(gameObject, delaySpawn+1); //destroy trigger after all
    }

    private IEnumerator WaitSpawnAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        eliteRoom.eliteReference.SetActive(true);
    }
}