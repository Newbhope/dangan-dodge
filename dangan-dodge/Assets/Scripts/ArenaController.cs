using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;

/**
 * Overall script for the game and arena
 * */
public class ArenaController : NetworkBehaviour {
    public int countdownNumber;
    public float countdownInterval;
    public Text countdownText;
    public float roundEndPauseTime;

    public Text playerOneScoreText;
    public Image playerOneBombs;
    public Text playerTwoScoreText;
    public Image playerTwoBombs;

    public int bombHorizontalPadding;
    public int startingBombCount = 3;

    public Text gameOverText;

    void Start() {
        UpdateScoreUi();
        //TODO: move somewhere better. maybe gamestats?
        GameStats.playerBombs[1] = startingBombCount;
        GameStats.playerBombs[2] = startingBombCount;
        //TODO: this gross dewd
        UpdateBombUi();

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

    IEnumerator RestartRound() {
        yield return new WaitForSeconds(roundEndPauseTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerExit2D(Collider2D other) {
		if (other != null) {
			Destroy(other.gameObject);
		}
	}

    internal void UpdateScoreUi() {
        int playerOneScore;
        GameStats.playerScores.TryGetValue(1, out playerOneScore);
        this.playerOneScoreText.text = "Score: " + playerOneScore;

        int playerTwoScore;
        GameStats.playerScores.TryGetValue(2, out playerTwoScore);
        playerTwoScoreText.text = "Score: " + playerTwoScore;
    }

    internal void UpdateBombUi() {
        playerOneBombs.fillAmount = (float) GameStats.playerBombs[1] / (float) startingBombCount;
        playerTwoBombs.fillAmount = (float) GameStats.playerBombs[2] / (float) startingBombCount;
    }

    public void CheckGameOver() {
        Dictionary<int, int> scores = GameStats.playerScores;
        //TODO: configurable score
        //there has to be a better way to do this
        if (scores.ContainsValue(7)) {
            foreach (var pair in scores) {
                if (pair.Value == 7) {
                    gameOverText.text = "Player " + pair.Key + " Wins!";
                }
            }
        } else {
            StartCoroutine(RestartRound());
        }
    }
}
