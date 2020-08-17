using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public void OnClickVersus()
    {
        SceneManager.LoadScene("Versus");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
