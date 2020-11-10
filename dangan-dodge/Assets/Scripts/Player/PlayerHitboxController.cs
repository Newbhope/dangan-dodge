using UnityEngine;

public class PlayerHitboxController : MonoBehaviour
{

    public GameObject explosionParticles;

    private BasePlayerVariables vars;
    private int hitPlayerId;

    private GameManager gameController;

    void Start()
    {
        vars = GetComponentInParent<BasePlayerVariables>();
        hitPlayerId = vars.playerId;
        gameController = FindObjectOfType<GameManager>();
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

            gameController.UpdateScoreUi();
            gameController.CheckGameOver();

            // Clear all bullets on death and create explosion particles
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }

            GameObject particleObject = Instantiate
                (explosionParticles,
                gameObject.transform.position,
                Quaternion.identity);
            particleObject.GetComponent<ParticleSystem>().Play();

            // Destroy the player prefab also instead of just the square prefab
            Destroy(this.transform.parent.gameObject);
        }
    }
}
