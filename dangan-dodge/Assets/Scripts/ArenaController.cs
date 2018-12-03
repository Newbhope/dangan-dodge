using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int startingBombCount = 1;

    public Text gameOverText;

    private NetworkManager manager;

    void Start() {
        manager = FindObjectOfType<NetworkManager>();

        //TODO: move somewhere better. maybe gamestats?
        GameStats.playerBombs[1] = startingBombCount;
        GameStats.playerBombs[2] = startingBombCount;
        UpdateScoreUi();
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

    void OnTriggerExit2D(Collider2D other) {
		if (other != null) {
			Destroy(other.gameObject);
		}
	}

    internal void UpdateScoreUi() {

        int playerOneScore;
        GameStats.playerScores.TryGetValue(1, out playerOneScore);
        playerOneScoreText.text = "Score: " + playerOneScore;

        int playerTwoScore;
        GameStats.playerScores.TryGetValue(2, out playerTwoScore);
        playerTwoScoreText.text = "Score: " + playerTwoScore;

        if (isServer) {
            RpcUpdateScoreUi(playerOneScore, playerTwoScore);
        }
    }

    //TODO: why do i need this
    [ClientRpc]
    private void RpcUpdateScoreUi(int serverOneScore, int serverTwoScore) {
        GameStats.playerScores[1] = serverOneScore;
        GameStats.playerScores[2] = serverTwoScore;
        playerOneScoreText.text = "Score: " + serverOneScore;
        playerTwoScoreText.text = "Score: " + serverTwoScore;
    }

    internal void UpdateBombUi() {
        playerOneBombs.fillAmount = (float) GameStats.playerBombs[1] / (float) 3;
        playerTwoBombs.fillAmount = (float) GameStats.playerBombs[2] / (float) 3;
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

    IEnumerator RestartRound() {
        yield return new WaitForSeconds(roundEndPauseTime);
        CmdRestartScene();
    }

    [Command]
    private void CmdRestartScene() {
        manager.ServerChangeScene(SceneManager.GetActiveScene().name);
    }
}
