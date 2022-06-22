/**
    Using Aron Granberg's AStar pathfinding:
    https://arongranberg.com/astar/
    Made with the help of Brackey's 2D pathfinding tutorial:
    https://www.youtube.com/watch?v=jvtFUfJ6CP8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieAI : Enemy
{
    private Player player;
    public Transform target;
    public Transform swingArea;
    public float speed = 50f;
    public float nextWaypointDistance = 2f;
    private bool facingRight = true;
    private bool swung = false;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Enemy self;

    Seeker seeker;
    public Rigidbody2D rb;

    void Start() {
        damage = 5;
        health = 100;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath() {
        if(seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

    }
    void OnPathComplete(Path p) {
         if(!p.error) {
             path = p; 
             currentWaypoint = 0;
         }
    }

    void FixedUpdate() {
        if(path == null ) {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }
        
        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;
        if (force.y > 0){
            force.y = 0;
        }

        rb.velocity = force;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance) {
            currentWaypoint++; 
        }

        if (facingRight == false && target.position.x > transform.position.x) {
            Flip();
        }
        else if (facingRight == true && target.position.x < transform.position.x) {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        player = hitInfo.GetComponent<Player>();
        InvokeRepeating("Swing", 1.5f, 2f);
    }
    void OnTriggerExit2D(Collider2D hitInfo) {
        CancelInvoke("Swing");
    }

    public void Swing() {
		if (player != null) {
			player.TakeDamage(damage);
		}
    }
}
