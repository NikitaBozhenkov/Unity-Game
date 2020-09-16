using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Background") other.gameObject.SetActive(false);
    }
}