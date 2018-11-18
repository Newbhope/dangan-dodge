using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PieceOfCrap : NetworkBehaviour {

    private MainMenuUiController menuUiController;
    private Button LeftButton;
    private Button RightButton;
    private Button StartButton;

	void Start () {
        menuUiController = FindObjectOfType<MainMenuUiController>();
        LeftButton = GameObject.FindGameObjectWithTag("Left").GetComponent<Button>();
        RightButton = GameObject.FindGameObjectWithTag("Right").GetComponent<Button>();
        StartButton = GameObject.FindGameObjectWithTag("Start").GetComponent<Button>();

        LeftButton.onClick.AddListener(LeftButtonClick);
        RightButton.onClick.AddListener(RightButtonClick);
        StartButton.onClick.AddListener(StartButtonClick);
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
        if (isServer) {
            RpcStartGame();
        }
        else {
            CmdStartGame();
        }
    }

    private void StartGame() {
        SceneManager.LoadScene("Arena");
    }

    [Command]
    private void CmdStartGame() {
        RpcStartGame();
        StartGame();
    }

    [ClientRpc]
    private void RpcStartGame() {
        StartGame();
    }
}
