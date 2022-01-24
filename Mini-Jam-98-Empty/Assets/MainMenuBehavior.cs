using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button controlsButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        controlsButton.onClick.AddListener(ControlsScene);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ControlsScene()
    {
        SceneManager.LoadScene(3);
    }

    private void QuitGame()
    {
        Application.Quit();
    }


}
