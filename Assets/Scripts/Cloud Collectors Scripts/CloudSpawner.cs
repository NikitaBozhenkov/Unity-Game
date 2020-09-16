using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] clouds;
    [SerializeField] private float distanceBetweenClouds = 3f;
    private float minX, maxX;
    private float lastCloudPosY;
    private float controlX;
    [SerializeField] private GameObject[] collectables;
    private GameObject player;

    private void Awake() {
        controlX = 0;
        SetMinAndMaxX();
        CreateClouds();
        player = GameObject.Find("Player");

        foreach (var col in collectables) {
            col.SetActive(false);
        }
    }

    private void Start() {
        PositionThePlayer();
    }

    void Shuffle(GameObject[] array) {
        for (int i = 0; i < array.Length; ++i) {
            var randomIndex = Random.Range(i, array.Length);
            var temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void CreateClouds() {
        Shuffle(clouds);

        float posY = 0;

        foreach (var cloud in clouds) {
            var temp = cloud.transform.position;
            temp.y = posY;
            switch (controlX) {
                case 0:
                    temp.x = Random.Range(0.0f, maxX);
                    break;
                case 1:
                    temp.x = Random.Range(0.0f, minX);
                    break;
                case 2:
                    temp.x = Random.Range(1.0f, maxX);
                    break;
                case 3:
                    temp.x = Random.Range(-1.0f, minX);
                    break;
            }

            controlX = (controlX + 1) % 4;

            cloud.transform.position = temp;

            lastCloudPosY = posY;
            posY -= distanceBetweenClouds;
        }
    }

    void PositionThePlayer() {
        var darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        var cloudsInGame = GameObject.FindGameObjectsWithTag("Cloud");

        foreach (GameObject darkCloud in darkClouds) {
            if (!darkCloud.transform.position.y.Equals(0f)) continue;

            var t = darkCloud.transform.position;
            darkCloud.transform.position = new Vector3(
                cloudsInGame[0].transform.position.x,
                cloudsInGame[0].transform.position.y,
                cloudsInGame[0].transform.position.z);
            cloudsInGame[0].transform.position = t;
        }

        var temp = cloudsInGame[0].transform.position;

        for (int i = 1; i < cloudsInGame.Length; ++i) {
            if (temp.y < cloudsInGame[i].transform.position.y) {
                temp = cloudsInGame[i].transform.position;
            }
        }

        temp.y += 0.8f;

        player.transform.position = temp;
    }

    void SetMinAndMaxX() {
        var bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        maxX = bounds.x - 0.5f;
        minX = -maxX;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Cloud") && !other.CompareTag("Deadly")) return;
        if (!other.transform.position.y.Equals(lastCloudPosY)) return;

        Shuffle(clouds);
        Shuffle(collectables);

        var temp = other.transform.position;

        foreach (var cloud in clouds) {
            if (cloud.activeInHierarchy) continue;
            switch (controlX) {
                case 0:
                    temp.x = Random.Range(0.0f, maxX);
                    break;
                case 1:
                    temp.x = Random.Range(0.0f, minX);
                    break;
                case 2:
                    temp.x = Random.Range(1.0f, maxX);
                    break;
                case 3:
                    temp.x = Random.Range(-1.0f, minX);
                    break;
            }

            temp.y -= distanceBetweenClouds;
            lastCloudPosY = temp.y;

            cloud.transform.position = temp;
            cloud.SetActive(true);

            var random = Random.Range(0, collectables.Length);
            if (cloud.tag != "Deadly") {
                if (!collectables[random].activeInHierarchy) {
                    var temp2 = cloud.transform.position;
                    temp2.y += 0.7f;
                    if (collectables[random].tag == "Life") {
                        if (PlayerScore.lifeCount < 2) {
                            collectables[random].transform.position = temp2;
                            collectables[random].SetActive(true);
                        }
                    } else {
                        collectables[random].transform.position = temp2;
                        collectables[random].SetActive(true);
                    }
                }
            }
        }
    }
}