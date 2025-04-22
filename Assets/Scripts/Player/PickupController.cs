/*****************************************************************************
// File Name : PickupController.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles all pickup items, their weight, the 
    damage they build, and the damage they deal to enemies.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private GameManager gm;
    
    //Used to calculate damage
    public float weight;
    public bool flying;
    public float damage;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// will allow the player to pick up the object if within pick up distance
    /// </summary>
    /// <param name="collision">only searches for player tag</param>
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().canPickup = true;
            collision.GetComponent<PlayerController>().possiblePickUp = this.gameObject;
        }
    }

    /// <summary>
    /// will remove the players ability to pick up this object if too far away
    /// </summary>
    /// <param name="collision">only searches for player</param>
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().canPickup = false;
            collision.GetComponent<PlayerController>().possiblePickUp = null;
        }
    }

    public void Throw(float throwStrength)
    {
        damage = throwStrength * weight;
    }

    /// <summary>
    /// damages enemies, not the player, then resets damage (will also reset damage if anything else is hit too)
    /// </summary>
    /// <param name="collision">looks for a collision that isnt the player and is an enemy</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Player")
        {
            if (collision.transform.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().health -= Mathf.RoundToInt(damage);
            }
            else
            {
                flying = false;
                damage = 0;
            }
        }
    }
}
