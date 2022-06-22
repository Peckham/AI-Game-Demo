using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : Enemy
{   
    public Rigidbody2D rb;
    public Transform playerCheck;
    public Transform biteRange;
    public LayerMask playerLayer;
    private bool facingRight = false;
    private bool dove = false;
    public float diveVelocity = 35f;
    public float hoverVelocity = 20f;
    public float patrolVelocity = 10f;
    public float hoverWait = 1f;
    public float patrolWait = 5f;
    public float checkRadius = 1f;

    void Start() {
        health = 25;
        damage = 5;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Float", 0.1f, hoverWait);
        InvokeRepeating("Patrol", 0.1f, patrolWait);
    }

    void FixedUpdate() {
        if(Physics2D.OverlapCircle(playerCheck.position, checkRadius, playerLayer)){
            Dive();
        }
       
        if (facingRight == false && patrolVelocity > 0) {
            Flip();
        }
        else if (facingRight == true && patrolVelocity < 0) {
            Flip();
        }
    }

    void Dive() {
         rb.velocity = new Vector2(rb.velocity.x, -diveVelocity);
         dove = true;
    }

    void Patrol() {
        patrolVelocity *= -1;
        rb.velocity = new Vector2(patrolVelocity, rb.velocity.y);
    }

    void Float() {
        if(dove) {
           rb.velocity = new Vector2(rb.velocity.x, diveVelocity);
           dove = false;
        } else {
            rb.velocity = new Vector2(rb.velocity.x, hoverVelocity);
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
		Player player = hitInfo.GetComponent<Player>();
		if (player != null) {
			player.TakeDamage(damage);
		}
	}
}
