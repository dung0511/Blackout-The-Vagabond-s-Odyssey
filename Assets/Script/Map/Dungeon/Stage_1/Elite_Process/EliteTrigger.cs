using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteTrigger : MonoBehaviour
{
    public GameObject spawnAnimation;
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
            EnableElite(); //appear animation then re enable elite
        }
    }

    private void EnableElite()
    {
        var anim = Instantiate(spawnAnimation, eliteRoom.eliteReference.transform.position, Quaternion.identity);
        StartCoroutine(WaitSpawnAnimation(0.5f));
        Destroy(anim, 1f);
        Destroy(gameObject, 1.5f); //destroy trigger after all
    }

    private IEnumerator WaitSpawnAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        eliteRoom.eliteReference.SetActive(true);
    }
}