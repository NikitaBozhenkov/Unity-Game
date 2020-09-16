using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HIghscoreController : MonoBehaviour {
    [SerializeField] private Text scoreText, coinText; 
    
    void Start() {
        SetTheScoreBasedOnDiff();
    }

    void SetScore(int score, int coinScore) {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
    }

    void SetTheScoreBasedOnDiff() {
        if (GamePreferences.GetEasyDifficulty() == 1) {
             SetScore(GamePreferences.GetEasyDifficultyScore(), GamePreferences.GetEasyDifficultyCoinScore());
        } else if (GamePreferences.GetMediumDifficulty() == 1) {
            SetScore(GamePreferences.GetMediumDifficultyScore(), GamePreferences.GetMediumDifficultyCoinScore());
        } else if (GamePreferences.GetHardDifficulty() == 1) {
            SetScore(GamePreferences.GetHardDifficultyScore(), GamePreferences.GetHardDifficultyCoinScore());
        }
    }
    
    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}