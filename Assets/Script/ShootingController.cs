using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Camera arCamera;
    public GameObject muzzleFlash; // Optional

    void Update()
    {
        // Mouse click (for Unity Editor testing)
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
        // Touch input (for mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Shoot();
            }
        }
        
        // Gamepad input - checks all buttons 0-19
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.JoystickButton0 + i)))
            {
                Debug.Log("Gamepad Button " + i + " pressed during gameplay!");
                Shoot();
                break;
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Shooting!");
        
        Ray ray = arCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            BotHealth botHealth = hit.collider.GetComponentInParent<BotHealth>();
            
            if (botHealth != null)
            {
                string bodyPart = hit.collider.tag;
                botHealth.TakeDamage(bodyPart);
                Debug.Log("Hit: " + bodyPart);
            }
            else
            {
                Debug.Log("Hit something but not a bot");
            }
        }
        else
        {
            Debug.Log("Missed - no hit");
        }
    }
}