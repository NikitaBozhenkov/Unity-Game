using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {
    void Start() {
        var sr = GetComponent<SpriteRenderer>();
        var tempScale = transform.localScale;

        var width = sr.sprite.bounds.size.x;
        var worldHeight = Camera.main.orthographicSize * 2f;
        var worldWidth = worldHeight / Screen.height * Screen.width;

        tempScale.x = worldWidth / width;
        transform.localScale = tempScale;
    }
    
}