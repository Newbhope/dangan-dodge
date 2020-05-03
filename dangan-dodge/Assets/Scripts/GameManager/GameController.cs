using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

/**
 * Overall script for the game and arena
 * */
public class GameController : MonoBehaviour {

    public int countdownNumber;
    public float countdownInterval;
    public Text countdownText;
    public float roundEndPauseTime;

    public GameObject playerOne;
    public GameObject playerTwo;

    public GameObject gameUI;
    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    public Text gameOverText;

    public GameObject pauseMenu;

    void Start() {
        UpdateScoreUi();
        StartCoroutine(StartRound());
    }

    // UI Stuff
    internal void UpdateScoreUi() {
        GameStats.playerScores.TryGetValue(0, out int playerOneScore);
        playerOneScoreText.text = "Score: " + playerOneScore;

        GameStats.playerScores.TryGetValue(1, out int playerTwoScore);
        playerTwoScoreText.text = "Score: " + playerTwoScore;
    }

    // Game Manager Stuff

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

    public void PausePressed() {
        switch (Time.timeScale) {
            case 1:
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                break;
            case 0:
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                break;
        }
    }

    public void OnClickTitle() {
        SceneManager.LoadScene("MainMenu");
    }
}
