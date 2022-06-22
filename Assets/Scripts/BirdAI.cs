using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BirdAI : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float nextWaypointDistance = 2f;
    public bool trackPlayer = true;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public bool facingRight = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath() {
        if(seeker.IsDone()) {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }

    }
    void OnPathComplete(Path p) {
         if(!p.error) {
             path = p; 
             currentWaypoint = 0;
         }
    }

    void FixedUpdate() {
        if(path == null) {
            return;
        }

        if(trackPlayer) { 
            if(currentWaypoint >= path.vectorPath.Count) {
                reachedEndOfPath = true;
                return;
            } else {
                reachedEndOfPath = false;
            }

            Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            
            if(path.vectorPath.Count < 5) {
                rb.velocity = new Vector2(-direction.x, Mathf.Abs(direction.y)) * speed;
                Destroy(gameObject, 4);
            }

            if(distance < nextWaypointDistance) {
                currentWaypoint++;
            }

            if (facingRight == false && rb.velocity.x > 0) {
                Flip();
            }
            else if (facingRight == true && rb.velocity.x < 0) {
                Flip();
            }
        }
    }

    void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
