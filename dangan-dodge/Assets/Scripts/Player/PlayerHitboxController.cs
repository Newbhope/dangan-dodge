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

    void OnTriggerEnter2D(Collider2D other) {
        //TODO: Consider making this unit testable by extracting logic to an outside class/method

		BaseBulletVariables bulletVars = other
			.gameObject
			.GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;

        int shootingPlayerNumber = bulletVars.playerNumberInt;

        //A bullet that isn't owned by the player
        if (bulletVars != null && shootingPlayerNumber != hitPlayerNumber) {
            int currentScore;
            GameStats.playerScores.TryGetValue(shootingPlayerNumber, out currentScore);
            currentScore += 1;
            GameStats.playerScores[shootingPlayerNumber] = currentScore;

            arenaController.UpdateScoreUi();
            arenaController.CheckGameOver();

            GameObject particleObject = Instantiate
                (explosionParticles, 
                gameObject.transform.position,
                Quaternion.identity);
            particleObject.GetComponent<ParticleSystem>().Play();

            Destroy(other.gameObject);
            //Destroy the player prefab instead of the square prefab
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
