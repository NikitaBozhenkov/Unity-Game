using System.Collections;
using System.Collections.Generic;
using UnityEngine;    

public class CameraScript : MonoBehaviour {
    private float speed = 1f;
    private float acceleration = 0.2f;

    private float maxSpeed = 3.2f;
    private float easyMaxSpeed = 3.5f;
    private float mediumMaxSpeed = 4f;
    private float hardMaxSpeed = 4.5f;

    [HideInInspector] public bool moveCamera;

    // Start is called before the first frame update
    void Start() {
        if (GamePreferences.GetEasyDifficulty() == 1) maxSpeed = easyMaxSpeed;
        else if (GamePreferences.GetMediumDifficulty() == 1) maxSpeed = mediumMaxSpeed;
        else if (GamePreferences.GetHardDifficulty() == 1) maxSpeed = hardMaxSpeed;
        
        moveCamera = true;
    }

    // Update is called once per frame
    void Update() {
        if (moveCamera) {
            MoveCamera();
        }
    }

    void MoveCamera() {
        var temp = transform.position;

        var oldY = temp.y;
        var newY = oldY - speed * Time.deltaTime;

        temp.y = Mathf.Clamp(temp.y, oldY, newY);
        transform.position = temp;

        speed += acceleration * Time.deltaTime;

        if (speed > maxSpeed) speed = maxSpeed;
    }
}