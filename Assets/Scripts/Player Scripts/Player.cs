using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 8f;
    public float maxVelocity = 4f;
    
    private Rigidbody2D body;
    private Animator animator;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        var forceX = 0f;
        var vel = Mathf.Abs(body.velocity.x);

        var h = Input.GetAxis("Horizontal");

        if (h > 0) {
            if (vel < maxVelocity) forceX = speed;
            animator.SetBool("Walk", true);

            var temp = transform.localScale;
            temp.x = 1.3f;
            transform.localScale = temp;

        } else if (h < 0) {
            if (vel < maxVelocity) forceX = -speed;
            animator.SetBool("Walk", true);
            
            Vector3 temp = transform.localScale;
            temp.x = -1.3f;
            transform.localScale = temp;
        } else {
            animator.SetBool("Walk", false);  
        }
        
        body.AddForce(new Vector2(forceX, 0));
    }
}