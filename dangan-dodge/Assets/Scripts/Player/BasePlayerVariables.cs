using Photon.Pun;
using UnityEngine;

public class BasePlayerVariables : MonoBehaviourPunCallbacks
{
    public int playerId;
    public int bombsLeft;

    // Sadly property fields can't be exposed by the default Unity editor
    public int Energy
    {
        get
        {
            return energy;
        }

        set
        {
            energy = value;
            if (gameController != null)
            {
                gameController.UpdateEnergyUi();
            }
        }
    }
    private int energy;

    private GameManager gameController;

    void Awake()
    {
        gameController = FindObjectOfType<GameManager>();
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
        // Destroy the player prefab also instead of just the square prefab
        PhotonNetwork.Destroy(this.transform.parent.gameObject);
        Debug.LogError("plzzzz");
    }
}
