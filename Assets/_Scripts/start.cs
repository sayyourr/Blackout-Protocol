using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    public Button pressedButton;
    public int index;


    void Start()
    {
        pressedButton.onClick.AddListener(OnStartClicked);
    }

    public void LoadSceneByIndex(int Index){
        SceneManager.LoadScene(Index);
    }

    void OnStartClicked()
    {
        Debug.Log("Start button clicked!");
        index++;
        SceneManager.LoadScene(index);
    }
}
