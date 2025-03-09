using System.Collections.Generic;
using UnityEngine;

public class ChairLmao : MonoBehaviour
{
    [SerializeField] private List<GameObject> singleChairs;
    [SerializeField] private List<GameObject> longChairs;
    [SerializeField] private int chairChance = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<GameObject> chairsPoint = getChildren(this.transform);
        List<GameObject> pointChildren = new List<GameObject>(0);
        foreach(GameObject point in chairsPoint)
        {
            if(Utility.UnseededRng(0,100) < chairChance)
            {
                if(point.transform.childCount > 0)
                {
                    if(Utility.UnseededRng(0,100) < 50)
                    {
                        pointChildren = getChildren(point.transform);
                        foreach(GameObject chair in pointChildren)
                        {
                            Instantiate(singleChairs[Utility.UnseededRng(0, singleChairs.Count)], chair.transform.position, Quaternion.identity, chair.transform);
                        }
                    } else {
                        Instantiate(longChairs[Utility.UnseededRng(0, longChairs.Count)], point.transform.position, Quaternion.identity, point.transform);
                    }
                } else {
                    Instantiate(singleChairs[Utility.UnseededRng(0, singleChairs.Count)], point.transform.position, Quaternion.identity, point.transform);
                }
            }
            
            
        }
    }

    private List<GameObject> getChildren(Transform transform)
    {
        List<GameObject> children = new();
        foreach(Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }


}
