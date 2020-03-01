using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    public Button restartButton;


    public void OnClickLocal() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
