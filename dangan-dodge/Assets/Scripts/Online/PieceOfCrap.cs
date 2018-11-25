using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/**
 * Pretty weird class that handles player networking behaviour.
 **/
public class PieceOfCrap : NetworkBehaviour {

    private OnlineMenuUiController menuUiController;
    private Button LeftButton;
    private Button RightButton;
    private Button StartButton;
    private NetworkManager manager;

    public GameObject playerPrefab;

    void Start () {
        if (SceneManager.GetActiveScene().name == "OnlineMenu") {
            menuUiController = FindObjectOfType<OnlineMenuUiController>();
            LeftButton = GameObject.FindGameObjectWithTag("Left").GetComponent<Button>();
            RightButton = GameObject.FindGameObjectWithTag("Right").GetComponent<Button>();
            StartButton = GameObject.FindGameObjectWithTag("Start").GetComponent<Button>();

            LeftButton.onClick.AddListener(LeftButtonClick);
            RightButton.onClick.AddListener(RightButtonClick);
            StartButton.onClick.AddListener(StartButtonClick);

            manager = FindObjectOfType<NetworkManager>();
        }
        if (SceneManager.GetActiveScene().name == "Arena" && 
            isLocalPlayer &&
            (PlayerStats.playerNum == 1 || PlayerStats.playerNum == 2)) {

            CmdPopulatePlayers(PlayerStats.playerNum);
        }
    }

    [Command]
    private void CmdPopulatePlayers(int playerNum) {
        if (playerNum == 1) {
            GameObject leftPlayerObject = Instantiate(
                playerPrefab,
                new Vector3(-15, 0, 0),
                Quaternion.identity);
            BasePlayerVariables leftVars = leftPlayerObject.GetComponent<BasePlayerVariables>();
            leftVars.playerNumberInt = 1;
            leftVars.playerVector = new Vector2(1, 0);
            leftVars.isFlipped = false;

            NetworkServer.SpawnWithClientAuthority(leftPlayerObject, connectionToClient);
        }
        if (playerNum == 2) {
            GameObject rightPlayerObject = Instantiate(
                playerPrefab,
                new Vector3(15, 0, 0),
                Quaternion.identity);
            BasePlayerVariables rightVars = rightPlayerObject.GetComponent<BasePlayerVariables>();
            rightVars.playerNumberInt = 2;
            rightVars.playerVector = new Vector2(-1, 0);
            rightVars.isFlipped = true;

            NetworkServer.SpawnWithClientAuthority(rightPlayerObject, connectionToClient);
        }
    }

    public void LeftButtonClick() {
        if (isLocalPlayer) {
            CmdChangeLeftState(true);
            PlayerStats.playerNum = 1;
        }
    }

    [Command]
    public void CmdChangeLeftState(bool newLeftState) {
        menuUiController.LeftButtonSelected = newLeftState;
    }

    public void RightButtonClick() {
        if (isLocalPlayer) {
            CmdChangeRightState(true);
            PlayerStats.playerNum = 2;
        }
    }

    [Command]
    public void CmdChangeRightState(bool newRightState) {
        menuUiController.RightButtonSelected = newRightState;
    }

    public void StartButtonClick() {
        CmdStartGame();
    }

    [Command]
    private void CmdStartGame() {
        manager.ServerChangeScene("Arena");
    }
}
