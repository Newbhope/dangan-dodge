using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectController : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    private Text playerListText;
    
    void Start()
    {
        playerListText = GameObject.FindGameObjectWithTag("PlayerListText").GetComponent<Text>();

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            UpdatePlayerList();
        }
    }

    void UpdatePlayerList()
    {
        StringBuilder stringBuilder = new StringBuilder();
        Dictionary<int, Player> ay = PhotonNetwork.CurrentRoom.Players;
        Debug.Log(ay);

        SortedDictionary<int, Player> lmao = new SortedDictionary<int, Player>(ay);

        foreach (KeyValuePair<int, Player> entry in lmao)
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
}
