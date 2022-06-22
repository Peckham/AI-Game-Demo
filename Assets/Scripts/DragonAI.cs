/**
	Made with the help of Brackey's 2D Shooting tutorial:
	https://www.youtube.com/watch?v=wkKsl1Mfp5M&list=PL04tHwWlDDmcpymHMSUbz3OOHaF41XBtk&index=6&t=540s
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : Enemy
{	
	public Player player;
	public Transform firePoint;
	public GameObject fireBlast;
	public int chargeRate = 1;
	public int fireCharge = 75;
	public int shootingRange = 210;
	public bool playerInRange = false;
	public float playerDistance;


	void Start() {
		health = 300;
		player = FindObjectOfType<Player>();
	}
	public void TakeDamage (int damage) {
		health -= damage;

		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate () {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
		if (playerDistance < shootingRange) {
			playerInRange = true;
		} else {
			playerInRange = false;
		}

		if (fireCharge >= 100 && playerInRange) {
			Shoot();
			fireCharge = 0;
		} else {
			if(fireCharge < 100){
				fireCharge += chargeRate;
			}
			
		}
	}

	void Shoot () {
		Instantiate(fireBlast, firePoint.position, firePoint.rotation);
	}
}
