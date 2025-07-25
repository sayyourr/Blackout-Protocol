using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathMenu : MonoBehaviour
{
 private Button restartButton;
    private Button mainMenuButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        restartButton = root.Q<Button>("restart-button");
        mainMenuButton = root.Q<Button>("mainmenu-button");

        restartButton?.RegisterCallback<ClickEvent>(OnRestartClicked);
        mainMenuButton?.RegisterCallback<ClickEvent>(OnMainMenuClicked);
    }

    void OnRestartClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnMainMenuClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
    }
}
