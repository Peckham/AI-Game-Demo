/**    
    Using AStar pathfinding:
    https://arongranberg.com/astar/
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MagicMalletAI : Enemy
    {
    public Transform target;
    public float speed = 50f;
    public float nextWaypointDistance = 2f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Vector2 wanderVector = new Vector2(0,0);
    Seeker seeker;
    Rigidbody2D rb;
    public float wanderRange = 5f;
    public static int crushCounter = 50;
    private int crush = crushCounter;
    private bool crushed = false;

    void Start() {
        health = 125;
        damage = 10;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("WanderValue", 0f, 1f);
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
        if(path == null ) {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count) {
                reachedEndOfPath = true;
                return;
        } else {
                reachedEndOfPath = false;
        }

        switch(path.vectorPath.Count) {
            case int n when (crushed) :
                if (crush < crushCounter) {
                    crush++;
                } else {
                    crushed = false;
                }
                break;

            case int n when (n > 10) :
                InvokeRepeating("WanderAimlessly", 0f, 1.5f);
                break;

            case int n when (n <= 10 && n > 5) :
                CancelInvoke("WanderAimlessly");
                Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
                rb.velocity = direction * speed * 0.75f + wanderVector / 2;
                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if(distance < nextWaypointDistance) {
                    currentWaypoint++;
                }
                break;

            case int n when (n <= 5 && n >= 2 && crush > 0):
                CancelInvoke("WanderAimlessly");
                Vector2 direction1 = (path.vectorPath[currentWaypoint] - transform.position).normalized;
                rb.velocity = direction1 * speed;
                float distance1 = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if(distance1 < nextWaypointDistance) {
                    currentWaypoint++;
                }
                if(n == 2 && crush > 0) {
                    crush--;
                }
                break;

            case int n when (n <= 2 && crush == 0) :
                rb.velocity = new Vector2(0, -200);
                crushed = true;
                break;
        }
    }

    void WanderAimlessly() {
        rb.velocity = wanderVector;
    }

    void WanderValue() {
        wanderVector = new Vector2(Random.Range(-wanderRange, wanderRange), Random.Range(-wanderRange, wanderRange));
    }
    void OnTriggerEnter2D (Collider2D hitInfo) {
		Player player = hitInfo.GetComponent<Player>();
		if (player != null) {
			player.TakeDamage(damage);
		}
	}
}
