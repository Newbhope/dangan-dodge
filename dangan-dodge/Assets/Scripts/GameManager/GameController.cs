using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

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

    public Text playerOneEnergy;
    public Slider playerOneEnergyMeter;
    public Text playerTwoEnergy;
    public Slider playerTwoEnergyMeter;

    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    public Text gameOverText;
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button why;

    private List<BasePlayerVariables> playerVars;

    void Start() {
        playerVars = new List<BasePlayerVariables> {
            playerOne.GetComponent<BasePlayerVariables>(),
            playerTwo.GetComponent<BasePlayerVariables>()
        };

        UpdateScoreUi();
        StartCoroutine(StartRound());


        // START TEST CODE
        playerOne.GetComponent<BasePlayerVariables>().Energy = 50;
        playerTwo.GetComponent<BasePlayerVariables>().Energy = 50;
        UpdateEnergyUi();
        // END TEST CODE
    }

    // UI Stuff

    internal void UpdateScoreUi() {
        GameStats.playerScores.TryGetValue(0, out int playerOneScore);
        playerOneScoreText.text = "Score: " + playerOneScore;

        GameStats.playerScores.TryGetValue(1, out int playerTwoScore);
        playerTwoScoreText.text = "Score: " + playerTwoScore;
    }

    internal void UpdateEnergyUi() {
        int p1EnergyValue = playerVars[0].Energy;
        playerOneEnergy.text = p1EnergyValue.ToString();
        playerOneEnergyMeter.value = p1EnergyValue;

        int p2EnergyValue = playerVars[1].Energy;
        playerTwoEnergy.text = p2EnergyValue.ToString();
        playerTwoEnergyMeter.value = p2EnergyValue;
    }

    // This is probably optimizng too much. Unused code now
    internal void UpdateEnergyUi(int playerId, int energy) {
        switch(playerId) {
            case 0:
                playerOneEnergy.text = energy.ToString();
                break;
            case 1:
                playerTwoEnergy.text = energy.ToString();
                break;
        }
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
                resumeButton.Select();
                break;
            case 0:
                Time.timeScale = 1;
                why.Select();
                pauseMenu.SetActive(false);
                break;
        }
    }

    public void OnClickTitle() {
        SceneManager.LoadScene("MainMenu");
    }
}
