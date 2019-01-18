using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class MainMenuUiController : MonoBehaviour {

    public Button local;
    public Button online;

    private NetworkManager manager;
    private OnlineMenuUiController menuController;

    private void Start() {
        manager = FindObjectOfType<NetworkManager>();
    }

    public void OnClickLocal() {
        SceneManager.LoadScene("LocalArena");
    }

    public void OnClickQuit() {
        Application.Quit();
    }

    public void HostLobby() {
        manager.StartMatchMaker();
        manager.matchMaker.CreateMatch("name", 4, true, "", "", "", 0, 0, OnCreateMatch);
    }

    private void OnCreateMatch(bool success, string extendedInfo, MatchInfo responseData) {
        manager.StartHost(responseData);
        Debug.LogError(manager.numPlayers);
    }

    public void EndLobby() {
        manager.StopServer();
    }

    public void FindLobby() {
        manager.StartMatchMaker();
        manager.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData) {
        Debug.LogError(responseData[0].name);
        manager.matchMaker.JoinMatch(responseData[0].networkId, "", "", "", 0, 0, OnMatchJoin);
    }

    private void OnMatchJoin(bool success, string extendedInfo, MatchInfo responseData) {
        manager.StartClient(responseData);
    }
}
