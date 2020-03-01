using UnityEngine;
using UnityEngine.UI;

public class PlayerHitboxController : MonoBehaviour {

    //TOOD: kinda weird that this is here
    public Text shootingPlayerScoreText;
    public GameObject explosionParticles;

    private BasePlayerVariables vars;
	private int hitPlayerNumber;

    private ArenaController arenaController;

    void Start () {
		vars = GetComponentInParent<BasePlayerVariables>();
		hitPlayerNumber = vars.playerNumberInt;
        arenaController = FindObjectOfType<ArenaController>();
    }

    void OnTriggerEnter2D(Collider2D collidingBullet) {
        // TODO: Consider making this unit testable by extracting logic to an outside class/method

		BaseBulletVariables bulletVars = collidingBullet
			.gameObject
			.GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;

        int shootingPlayerNumber = bulletVars.playerNumberInt;

        // A bullet that isn't owned by the player
        if (bulletVars != null && shootingPlayerNumber != hitPlayerNumber) {
            GameStats.playerScores.TryGetValue(shootingPlayerNumber, out int currentScore);
            currentScore += 1;
            GameStats.playerScores[shootingPlayerNumber] = currentScore;
            shootingPlayerScoreText.text = "Score: " + currentScore;
            arenaController.checkGameOver();


            // Clear all bullets on death and create explosion particles
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets) {
                Destroy(bullet);
            }

            GameObject particleObject = Instantiate
                (explosionParticles, 
                gameObject.transform.position,
                Quaternion.identity);
            particleObject.GetComponent<ParticleSystem>().Play();

            Destroy(collidingBullet.gameObject);
            // Destroy the player prefab also instead of just the square prefab
            Destroy(gameObject);
        }
    }
}
