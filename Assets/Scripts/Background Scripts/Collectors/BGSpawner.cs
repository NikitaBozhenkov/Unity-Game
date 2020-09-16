using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour {
    private GameObject[] backgrounds;
    private float lastY;
    
    void Start() {
        GetBackgroundsAndSetLastY();
    }

    void GetBackgroundsAndSetLastY() {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        lastY = backgrounds[0].transform.position.y;

        foreach (GameObject bg in backgrounds) {
            var curY = bg.transform.position.y;
            if (lastY > curY) lastY = curY;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Background")) return;
        if (!other.transform.position.y.Equals(lastY)) return;
        
        var temp = other.transform.position;
        var height = ((BoxCollider2D) other).size.y;

        foreach (var bg in backgrounds) {
            if (bg.activeInHierarchy) continue;
            temp.y -= height;
            lastY = temp.y;

            bg.transform.position = temp;
            bg.SetActive(true);

        }
    }
}