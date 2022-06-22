/**
    Using Aron Granberg's AStar pathfinding:
    https://arongranberg.com/astar/
    Made with the help of Brackey's 2D Shooting tutorial:
	https://www.youtube.com/watch?v=wkKsl1Mfp5M&list=PL04tHwWlDDmcpymHMSUbz3OOHaF41XBtk&index=6&t=540s
    Made with the help of Brackey's 2D pathfinding tutorial:
    https://www.youtube.com/watch?v=jvtFUfJ6CP8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FireBlastAI : MonoBehaviour
{   
    private Player player;
    private Transform target;
	public int damage = 25;
    public float speed = 75f;
    public float nextWaypointDistance = 2f;
    public bool trackPlayer = true;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    public Rigidbody2D rb;

    void Start() {
        player = FindObjectOfType<Player>();
        target = player.transform;
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
        if(trackPlayer) { 
            if(path == null ) {
                return;
            }

            if(currentWaypoint >= path.vectorPath.Count) {
                reachedEndOfPath = true;
                return;
            } else {
                reachedEndOfPath = false;
            }

            Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;

            if(target.position.x > transform.position.x) {
                trackPlayer = false;
                Destroy(gameObject, 4);
                return;
            }

            rb.velocity = direction * speed;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if(distance < nextWaypointDistance) {
                currentWaypoint++; 
            }
        }
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
		Player player = hitInfo.GetComponent<Player>();
		if (player != null)
		{
			player.TakeDamage(damage);
		}
		Destroy(gameObject);
	}
}
