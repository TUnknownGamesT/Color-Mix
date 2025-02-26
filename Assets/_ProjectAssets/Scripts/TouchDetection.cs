using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    public float tapThreshold = 0.2f; // Maximum time for a tap (in seconds)
    private float mouseClickStartTime;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            mouseClickStartTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button
        {
            float mouseClickEndTime = Time.time;
            float mouseClickDuration = mouseClickEndTime - mouseClickStartTime;

            if (mouseClickDuration <= tapThreshold)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("CannonBall")))
                {
                    if (hit.collider.CompareTag("Cannon"))
                    {
                        hit.collider.GetComponent<CannonBhaviour>().Shoot();
                    }
                }
            }
        }
    }
}
