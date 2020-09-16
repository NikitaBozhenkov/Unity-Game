using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {
    public static GameplayController instance;

    [SerializeField] private Text scoreText, coinText, lifeText, gameOverScoreText, gameOverCoinText;
    [SerializeField] private GameObject pausePanel, gameOverPanel;
    [SerializeField] private GameObject readyButton;

    void Awake() {
        MakeInstance();
    }

    private void Start() {
        Time.timeScale = 0f;
    }

    void MakeInstance() {
        if (instance == null) instance = this;
    }

    public void ReadyToStartTheGame() {
        Time.timeScale = 1f;
        readyButton.SetActive(false);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Gameplay") {
            if (GameManager.instance.gameRestartedAfterPlayerDie) {
                SetScore(GameManager.instance.score);
                SetCoinScore(GameManager.instance.coinScore);
                SetLifeScore(GameManager.instance.lifeScore);

                PlayerScore.scoreCount = GameManager.instance.score;
                PlayerScore.lifeCount = GameManager.instance.lifeScore;
                PlayerScore.coinCount = GameManager.instance.coinScore;
            } else if (GameManager.instance.gameStartedFromGameMenu) {
                PlayerScore.scoreCount = 0;
                PlayerScore.lifeCount = 2;
                PlayerScore.coinCount = 0;

                SetScore(0);
                SetCoinScore(0);
                SetLifeScore(2);
            }
        }
    }

    public void GameOverShowPanel(int finalScore, int finalCoinScore) {
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = finalScore.ToString();
        gameOverCoinText.text = finalCoinScore.ToString();
        StartCoroutine(GameOverLoadMainMenu());
    }

    IEnumerator GameOverLoadMainMenu() {
        yield return new WaitForSeconds(3f);
        SceneFader.instance.LoadLevel("MainMenu");
    }

    IEnumerator PlayerDiedRestart() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Gameplay");
    }

    public void PlayerDiedRestartTheGame() {
        StartCoroutine(PlayerDiedRestart());
    }

    public void SetScore(int score) {
        scoreText.text = score.ToString();
    }

    public void SetCoinScore(int coinScore) {
        coinText.text = coinScore.ToString();
    }

    public void SetLifeScore(int lifeScore) {
        lifeText.text = "x" + lifeScore;
    }

    public void PauseTheGame() {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeTheGame() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame() {
        Time.timeScale = 1f;
        SceneFader.instance.LoadLevel("MainMenu");
    }
}