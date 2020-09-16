using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {
    [SerializeField] private AudioClip coinClip, lifeClip;
    private CameraScript cameraScript;

    private Vector3 prevPos;
    private bool countScore;

    public static int scoreCount;
    public static int lifeCount;
    public static int coinCount;

    private void Awake() {
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    // Start is called before the first frame update
    void Start() {
        prevPos = transform.position;
        countScore = true;
    }

    // Update is called once per frame
    void Update() {
        CountScore();
    }

    void CountScore() {
        if (countScore) {
            if (transform.position.y < prevPos.y) {
                ++scoreCount;
            }

            prevPos = transform.position;
            GameplayController.instance.SetScore(scoreCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Coin") {
            ++coinCount;
            scoreCount += 200;
            
            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetCoinScore(coinCount);

            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            other.gameObject.SetActive(false);
        } else if (other.tag == "Life") {
            ++lifeCount;
            scoreCount += 300;
            
            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetLifeScore(lifeCount);

            AudioSource.PlayClipAtPoint(lifeClip, transform.position);
            other.gameObject.SetActive(false);
        } else if (other.tag == "Bounds" || other.tag == "Deadly") {
            cameraScript.moveCamera = false;
            countScore = false;

            transform.position = new Vector3(500, 500, 0);
            --lifeCount;
            GameManager.instance.CheckGameStatus(scoreCount, coinCount, lifeCount);
        } 
    }
}