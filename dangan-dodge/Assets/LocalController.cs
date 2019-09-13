using UnityEngine;
using UnityEngine.Networking;

// To allow network code to work locally, create local hosted game for offline
public class LocalController : MonoBehaviour {

    private NetworkManager manager;

    void Start () {
        if (!GameStats.localGameStarted) {
            manager = FindObjectOfType<NetworkManager>();
            manager.StartHost();
            GameStats.localGameStarted = true;
        }
    }
}
