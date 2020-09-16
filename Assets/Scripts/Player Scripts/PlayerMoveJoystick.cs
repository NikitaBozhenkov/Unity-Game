using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJoystick : MonoBehaviour {
    public float speed = 8f;
    public float maxVelocity = 4f;
    
    private Rigidbody2D body;
    private Animator animator;

    private bool moveLeft, moveRight;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (moveLeft) {
            MoveLeft();
        }

        if (moveRight) {
            MoveRight();
        }
    }

    public void SetMoveLeft(bool moveLeft) {
        this.moveLeft = moveLeft; 
        this.moveRight = !moveLeft;
    }

    public void SetMoveRight(bool moveRight) {
        this.moveRight = moveRight;
        this.moveLeft =!moveRight;
    }

    public void StopMoving() {
        moveLeft = moveRight = false;
        animator.SetBool("Walk", false);
    }

    void MoveLeft() {
        var forceX = 0f;
        var vel = Mathf.Abs(body.velocity.x);
        
        if (vel < maxVelocity) forceX = -speed;
        animator.SetBool("Walk", true);
            
        Vector3 temp = transform.localScale;
        temp.x = -1.3f;
        transform.localScale = temp;
        body.AddForce(new Vector2(forceX, 0));
    }

    void MoveRight() {
        var forceX = 0f;
        var vel = Mathf.Abs(body.velocity.x);
        
        if (vel < maxVelocity) forceX = -speed;
        animator.SetBool("Walk", true);
            
        Vector3 temp = transform.localScale;
        temp.x = 1.3f;
        transform.localScale = temp;
        body.AddForce(new Vector2(forceX, 0));
        
    }
}