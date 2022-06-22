/**
    Made with the help of Brackey's 2D Character controller tutorial:
    https://www.youtube.com/watch?v=dwcT-Dch0bA&t=981s
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float hitpoints = 100f;
    public GameObject bullet;
    public Transform healthRemaining;
    public Transform shootPoint;
    public Transform shootUpPoint;
    public Transform shootDownPoint;
    public float speed = 50f;
    public float acceleration = 0f;
    public float jumpForce;
    private float moveInput;
    private float lookInput;
    public Rigidbody2D rb;
    public Animator animator;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 1f;
    public LayerMask whatIsGround;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        moveInput = Input.GetAxisRaw("Horizontal");
        lookInput = Input.GetAxisRaw("Vertical");

        if(moveInput == 0) {
            acceleration = 0;
        }
        else if (acceleration < 1.25) {
            acceleration += 0.05f;
        }

        rb.velocity = new Vector2(moveInput * (speed + acceleration), rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetFloat("Look", lookInput);

        if(Input.GetButtonDown("Jump") && isGrounded == true) {
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetButtonDown("Fire1")) {
            Shoot(lookInput);
        }

        if (facingRight == false && moveInput > 0) {
            Flip();
        } else if (facingRight == true && moveInput < 0) {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Shoot(float lookInput) {
        switch(lookInput) {
            case float n when (n == 0):
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                break;
            case float n when (n > 0):
                Instantiate(bullet, shootUpPoint.position, shootUpPoint.rotation);
                break;
            case float n when (n < 0):
                Instantiate(bullet, shootDownPoint.position, shootDownPoint.rotation);
                break;
        }
    }

    public void setHealthBarSize(float healthVal) {
        healthRemaining.localScale = new Vector3(healthVal / 100, 0.95f, 1f);
    }

    public void TakeDamage(int damage) {
        hitpoints -= damage;
        setHealthBarSize(hitpoints);
        if(hitpoints <= 0) {
            Destroy(gameObject);
        }
    }
}
