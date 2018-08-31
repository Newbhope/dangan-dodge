using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/**
 * Overall script for the game and arena
 * */
public class ArenaController : MonoBehaviour {

    public float roundEndPauseTime;

    public GameObject playerOne;
    public GameObject playerTwo;

    public Text playerOneScoreText;
    public Text playerTwoScoreText;

    void Start() {
        int playerOneScore;
        GameStats.playerScores.TryGetValue(1, out playerOneScore);
        playerOneScoreText.text = "Score: " + playerOneScore;

        int playerTwoScore;
        GameStats.playerScores.TryGetValue(2, out playerTwoScore);
        playerTwoScoreText.text = "Score: " + playerTwoScore;
    }

    void Update() {
        if (playerOne == null || playerTwo == null) {
            StartCoroutine(restartRound());
        }
	}

    IEnumerator restartRound() {
        yield return new WaitForSeconds(roundEndPauseTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerExit2D(Collider2D other) {
		if (other != null) {
			Destroy(other.gameObject);
		}
	}
}
