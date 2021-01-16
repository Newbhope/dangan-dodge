using Photon.Pun;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviourPunCallbacks
{

    public GameObject explosionParticles;

    private BasePlayerVariables vars;
    private int hitPlayerId;

    private GameManager gameController;
    private GameNetworkManager gameNetworkManager;

    void Start()
    {
        vars = GetComponentInParent<BasePlayerVariables>();
        hitPlayerId = vars.playerId;
        gameController = FindObjectOfType<GameManager>();
        gameNetworkManager = FindObjectOfType<GameNetworkManager>();
    }

    void OnTriggerEnter2D(Collider2D collidingBullet)
    {
        // TODO: Consider making this unit testable by extracting logic to an outside class/method

        BaseBulletVariables bulletVars = collidingBullet
            .gameObject
            .GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;

        // A bullet that isn't owned by the player
        if (bulletVars != null && bulletVars.ownerPlayerId != hitPlayerId)
        {

            GameStats.playerScores.TryGetValue(bulletVars.ownerPlayerId, out int currentScore);
            currentScore += 1;
            GameStats.playerScores[bulletVars.ownerPlayerId] = currentScore;


            // This might be needed since I was testing with an outdated client

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets)
            {
                PhotonNetwork.Destroy(bullet);
            }

            // TODO: fix
            GameObject particleObject = PhotonNetwork.Instantiate(
                explosionParticles.name,
                gameObject.transform.position,
                Quaternion.identity);
            particleObject.GetComponent<ParticleSystem>().Play();


            gameController.UpdateScoreUi();
            gameController.CheckGameOver();

            PhotonNetwork.Destroy(this.transform.parent.gameObject);

            gameNetworkManager.photonView.RPC("RPCDie", RpcTarget.All);
        }
    }
}
