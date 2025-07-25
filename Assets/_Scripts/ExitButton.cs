using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();

        // So i can confirm it works
        Debug.Log("Quit triggered");
    }
}
