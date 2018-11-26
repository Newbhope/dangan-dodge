using UnityEngine;
using UnityEngine.Networking;

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
