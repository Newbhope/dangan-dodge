using UnityEngine;
using UnityEngine.UI;

public class PlayerHitboxController : MonoBehaviour {

    public Text shootingPlayerScoreText;

    private BasePlayerVariables vars;
	private int hitPlayerNumber;

    private ParticleSystem pSystem;

    void Start () {
		vars = GetComponentInParent<BasePlayerVariables>();
		hitPlayerNumber = vars.playerNumberInt;

        pSystem = transform.parent.gameObject.GetComponent<ParticleSystem>();
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
            shootingPlayerScoreText.text = "Score: " + currentScore;
            //TODO use instantaite insead
            pSystem.Play();

            //Destroy the player prefab instead of the square prefab
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
