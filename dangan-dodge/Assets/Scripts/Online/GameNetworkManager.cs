using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNetworkManager : MonoBehaviourPun
{
    public Color playerTwoColorTest;

    private GameManager gameManager;
    private GameObject playerOneComponents;
    private GameObject playerTwoComponents;


    [Tooltip("The prefab to use for representing the player")]
    // OnlinePlayerComponents
    public GameObject playerPrefab;
    public GameObject playerTwoPrefab; // TODO: make this better
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

        if (PhotonNetwork.IsMasterClient) // bleh
        {
            playerOneComponents = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            //IntializePlayer(playerOneComponents);
        } 
        else
        {
            // Ideally I'd use spawn points I put in the editor for this
            playerTwoComponents = PhotonNetwork.Instantiate(this.playerTwoPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 180, 0), 0);
        }

    }

    [PunRPC]
    void RPCDie(PhotonMessageInfo info)
    {
        // Need this here to kill uh stuff you don't own

        // Clear all bullets on death and create explosion particles
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            PhotonNetwork.Destroy(bullet);
        }
        Debug.LogError("plz");
    }
    /*
    // GameObjects are OnlinePlayerComponents prefab
    private void InitializePlayers(GameObject playerComponents)
    {
        // ewwww
        GameObject playerOneShip = playerComponents.transform.GetChild(0).gameObject;
        GameObject playerOneGates = playerComponents.transform.GetChild(1).gameObject;
        GameObject playerOneGateStart = playerComponents.transform.GetChild(2).gameObject;
        GameObject playerOneGateEnd = playerComponents.transform.GetChild(3).gameObject;

        SpriteRenderer playerOneRenderer = playerOneShip.GetComponentInChildren<SpriteRenderer>();

        Color playerTwoColor = new Color(0, 235, 255);

        GameObject playerTwoShip = playerTwoComponents.transform.GetChild(0).gameObject;
        GameObject playerTwoGates = playerTwoComponents.transform.GetChild(1).gameObject;
        GameObject playerTwoGateStart = playerTwoComponents.transform.GetChild(2).gameObject;
        GameObject playerTwoGateEnd = playerTwoComponents.transform.GetChild(3).gameObject;

        SpriteRenderer playerTwoShipRenderer = playerTwoShip.GetComponentInChildren<SpriteRenderer>();
        playerTwoShipRenderer.color = playerTwoColor;

        SpriteRenderer[] playerTwoGatesRenderers = playerTwoGates.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer gateRenderer in playerTwoGatesRenderers)
        {
            gateRenderer.color = playerTwoColor;
        }



        gameManager.OnlineInitialize(playerOneShip, playerTwoShip);
    }
    */


    // use IPunInstantiateMagicCallback apparently
}
