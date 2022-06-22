/**
    Made with the help of Brackey's 2D Shooting tutorial:
	https://www.youtube.com/watch?v=wkKsl1Mfp5M&list=PL04tHwWlDDmcpymHMSUbz3OOHaF41XBtk&index=6&t=540s
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
