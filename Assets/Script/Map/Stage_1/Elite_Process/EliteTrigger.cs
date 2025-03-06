using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteTrigger : MonoBehaviour
{
    public GameObject spawnAnimation;
    [HideInInspector] public BoxRoom eliteRoom;
    [HideInInspector] public List<GameObject> barriers;

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach(var barrier in barriers) //block entrances
        {
            //animation 
            barrier.SetActive(true);
        }
        EnableElite(); //appear animation then re enable elite
    }

    private void EnableElite()
    {
        var anim = Instantiate(spawnAnimation, eliteRoom.eliteReference.transform.position, Quaternion.identity);
        var animator = anim.GetComponent<Animator>();
        StartCoroutine(WaitSpawnAnimation(0.5f));
        Destroy(anim, 2f);
    }

    private IEnumerator WaitSpawnAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        eliteRoom.eliteReference.SetActive(true);
    }
}