using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/**
 * Overall script for the game and arena
 * */
public class ArenaController : MonoBehaviour {

    public int countdownNumber;
    public float countdownInterval;
    public Text countdownText;
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

        StartCoroutine(StartRound());
    }

    IEnumerator StartRound() {
        Time.timeScale = 0;
        for (int currentNumber = countdownNumber; currentNumber > 0; currentNumber--) {
            countdownText.text = currentNumber.ToString();
            yield return new WaitForSecondsRealtime(countdownInterval);
        }
        countdownText.text = "DODGE!";
        yield return new WaitForSecondsRealtime(.4f);
        Destroy(countdownText);
        Time.timeScale = 1;
    }

    void Update() {
        if (playerOne == null || playerTwo == null) {
            StartCoroutine(RestartRound());
        }
	}

    IEnumerator RestartRound() {
        yield return new WaitForSeconds(roundEndPauseTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerExit2D(Collider2D other) {
		if (other != null) {
			Destroy(other.gameObject);
		}
	}
}
