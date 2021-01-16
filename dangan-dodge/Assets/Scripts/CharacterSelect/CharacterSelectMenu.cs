using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviourPunCallbacks
{

    private Text playerListText;

    public void Start()
    {
        playerListText = GameObject.FindGameObjectWithTag("PlayerListText").GetComponent<Text>();
        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        StringBuilder stringBuilder = new StringBuilder();
        Dictionary<int, Player> players = PhotonNetwork.CurrentRoom.Players;
        SortedDictionary<int, Player> sortedPlayers = new SortedDictionary<int, Player>(players);

        foreach (KeyValuePair<int, Player> entry in sortedPlayers)
        {
            stringBuilder.AppendLine((entry.Key + 1).ToString());
            stringBuilder.AppendLine(entry.Value.NickName);
        }

        playerListText.text = stringBuilder.ToString();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList();
    }

    public void OnClickBack()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public void OnClickFight()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.LoadLevel("Online");
        this.photonView.RPC("RPCFight", RpcTarget.All);
    }

    [PunRPC]
    void RPCFight(PhotonMessageInfo info)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("Online");
    }
}
