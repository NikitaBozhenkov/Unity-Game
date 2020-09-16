using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCollector : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Cloud") || other.CompareTag("Deadly"))
            other.gameObject.SetActive(false);
    }
}