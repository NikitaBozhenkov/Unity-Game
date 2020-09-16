using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    [HideInInspector] public bool gameStartedFromGameMenu, gameRestartedAfterPlayerDie;
    [HideInInspector] public int score, coinScore, lifeScore;

    // Start is called before the first frame update
    void Start() {
        MakeSingleton();
        InitializeVariables();
    }

    private void MakeSingleton() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeVariables() {
        if (!PlayerPrefs.HasKey("Game Initialized")) {
            GamePreferences.SetEasyDifficulty(0);
            GamePreferences.SetEasyDifficultyScore(0);
            GamePreferences.SetEasyDifficultyCoinScore(0);

            GamePreferences.SetMediumDifficulty(1);
            GamePreferences.SetMediumDifficultyScore(0);
            GamePreferences.SetMediumDifficultyCoinScore(0);

            GamePreferences.SetHardDifficulty(0);
            GamePreferences.SetHardDifficultyScore(0);
            GamePreferences.SetHardDifficultyCoinScore(0);

            GamePreferences.SetMusicState(0);
            PlayerPrefs.SetInt("Game Initialized", 42);
        } 
    } 

    public void CheckGameStatus(int score, int coinScore, int lifeScore) {
        if (lifeScore < 0) {
            if (GamePreferences.GetEasyDifficulty() == 1) {
                var highScore = GamePreferences.GetEasyDifficultyScore();
                var coinsHigh = GamePreferences.GetEasyDifficultyCoinScore();

                if (highScore < score) GamePreferences.SetEasyDifficultyScore(score);
                if (coinsHigh < coinScore) GamePreferences.SetEasyDifficultyCoinScore(coinScore);
            } else if (GamePreferences.GetMediumDifficulty() == 1) {
                var highScore = GamePreferences.GetMediumDifficultyScore();
                var coinsHigh = GamePreferences.GetMediumDifficultyCoinScore();

                if (highScore < score) GamePreferences.SetMediumDifficultyScore(score);
                if (coinsHigh < coinScore) GamePreferences.SetMediumDifficultyCoinScore(coinScore);
            } else if (GamePreferences.GetHardDifficulty() == 1) {
                var highScore = GamePreferences.GetHardDifficultyScore();
                var coinsHigh = GamePreferences.GetHardDifficultyCoinScore();

                if (highScore < score) GamePreferences.SetHardDifficultyScore(score);
                if (coinsHigh < coinScore) GamePreferences.SetHardDifficultyCoinScore(coinScore);
            }

            gameRestartedAfterPlayerDie = false;
            gameStartedFromGameMenu = false;

            GameplayController.instance.GameOverShowPanel(score, coinScore);
        } else {
            this.score = score;
            this.coinScore = coinScore;
            this.lifeScore = lifeScore;

            gameRestartedAfterPlayerDie = true;
            gameStartedFromGameMenu = false;

            GameplayController.instance.PlayerDiedRestartTheGame();
        }
    }
}