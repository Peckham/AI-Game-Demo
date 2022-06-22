/**
    Made with the help of Brackey's 2D Shooting tutorial:
	https://www.youtube.com/watch?v=wkKsl1Mfp5M&list=PL04tHwWlDDmcpymHMSUbz3OOHaF41XBtk&index=6&t=540s
*/
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public Transform shootPoint;
    public float speed = 100f;
    public int damage = 50; 
    public Rigidbody2D rb;
    
    void Start() {
        rb.velocity = transform.right * speed; 
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        Destroy(gameObject);

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
        }
    }
}