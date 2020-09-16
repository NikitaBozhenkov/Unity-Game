using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour {
    void DestroyCollecrable() {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        Invoke("DestroyCollecrable", 6f);
    }
}