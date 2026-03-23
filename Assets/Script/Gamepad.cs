using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamepadButton : MonoBehaviour
{
    private Button button;
    private bool wasPressed = false;

    void Start()
    {
        button = GetComponent<Button>();
        
        if (button == null)
        {
            Debug.LogError("GamepadButton requires a Button component!");
        }
    }

    void Update()
    {
        // Only process if this button is the active/selected one
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            // Check for gamepad button press
            for (int i = 0; i < 20; i++)
            {
                if (Input.GetKeyDown((KeyCode)((int)KeyCode.JoystickButton0 + i)))
                {
                    Debug.Log("Gamepad pressed on button: " + gameObject.name);
                    
                    if (button != null && button.interactable)
                    {
                        button.onClick.Invoke();
                    }
                    break;
                }
            }
        }
    }
}