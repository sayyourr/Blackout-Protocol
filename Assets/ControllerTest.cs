using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerTest : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                Debug.Log("X button pressed");

            Vector2 move = Gamepad.current.leftStick.ReadValue();
            Debug.Log("Left Stick: " + move);
        }
        else
        {
            Debug.Log("No gamepad connected.");
        }
    }
}