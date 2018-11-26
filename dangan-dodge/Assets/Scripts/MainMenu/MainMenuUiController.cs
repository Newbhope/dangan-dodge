using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUiController : MonoBehaviour {

    public Button local;
    public Button online;

    public void OnClickLocal() {
        SceneManager.LoadScene("LocalArena");
    }

    public void OnClickOnline() {
        SceneManager.LoadScene("OnlineMenu");
    }
}
