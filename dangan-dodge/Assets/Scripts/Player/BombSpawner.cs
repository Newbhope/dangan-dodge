using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BombSpawner : NetworkBehaviour {

    public int horizontalPadding;
    public Image originalBombIcon;
    public GameObject particles;

    private ArenaController arenaController;

    private void Start() {
        arenaController = FindObjectOfType<ArenaController>();
    }

    [Command]
    public void CmdSpawn(int bombsLeft, int playerNum) {
        Debug.Log("Bombs left: " + bombsLeft);

        //TODO: clean up to mimic bullet prefab
        RpcSpawn(bombsLeft, playerNum);

        bombsLeft--;
        GameStats.playerBombs[playerNum] = bombsLeft;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }
        GameObject particleGameObject = Instantiate(particles, gameObject.transform);
        particleGameObject.GetComponent<ParticleSystem>().Play();
        arenaController.UpdateBombUi();

    }

    [ClientRpc]
    internal void RpcSpawn(int bombsLeft, int playerNumberInt) {
        Debug.Log("Bombs left: " + bombsLeft);

        bombsLeft--;
        GameStats.playerBombs[playerNumberInt] = bombsLeft;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }
        GameObject particleGameObject = Instantiate(particles, gameObject.transform);
        particleGameObject.GetComponent<ParticleSystem>().Play();
        arenaController.UpdateBombUi();
    }
}
