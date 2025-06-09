using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Android;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            return;
        }
    }
    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    public void returnHome()
    {
        SceneManager.LoadScene(0);
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
