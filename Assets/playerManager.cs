using UnityEngine;
using UnityEngine.InputSystem;

public class playerManager : MonoBehaviour
{
    private int playerCount = 0;

    void OnEnable()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    void OnDisable()
    {
        PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerCount++;

        Camera cam = playerInput.camera;

        if (playerCount == 1)
        {
            cam.rect = new Rect(0f, 0.5f, 1f, 0.5f); // Top
        }
        else if (playerCount == 2)
        {
            cam.rect = new Rect(0f, 0f, 1f, 0.5f);   // Bottom
        }
    }
}
