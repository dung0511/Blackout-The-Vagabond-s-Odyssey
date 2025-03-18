using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject[] CullingObjectsParent; 

    void FixedUpdate()
    {
        float cameraHalfWidth = playerCamera.orthographicSize * ((float)Screen.width / (float)Screen.height); 
        float cameraRight = playerCamera.transform.position.x + cameraHalfWidth;
        float cameraLeft = playerCamera.transform.position.x - cameraHalfWidth;
        float cameraTop = playerCamera.transform.position.y + playerCamera.orthographicSize;
        float cameraBottom = playerCamera.transform.position.y - playerCamera.orthographicSize;

        foreach (GameObject parent in CullingObjectsParent)
        {
            if (parent == null) continue;
            
            foreach (Transform child in parent.transform)
            {
                Renderer objRenderer = child.GetComponent<Renderer>();
                if (objRenderer != null)
                {
                    Bounds bounds = objRenderer.bounds; // get sprite bounding box
                    float objLeft = bounds.min.x;
                    float objRight = bounds.max.x;
                    float objTop = bounds.max.y;
                    float objBottom = bounds.min.y;

                    // check any part of sprite in camera view
                    bool isInView = (objRight >= cameraLeft && objLeft <= cameraRight && 
                                     objTop >= cameraBottom && objBottom <= cameraTop); 

                    child.gameObject.SetActive(isInView);
                }
            }
        }
    }
}
