/*****************************************************************************
// File Name : EnemyController.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles enemies, their ai types, health, damaging
    the player, and their healthbars.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //general info
    private GameManager gm;
    public int health;
    private bool alive;
    private float deathTimer;

    //for healthbar
    [SerializeField] private Slider healthBar;
    private GameObject cam;
    
    //ai types
    public int aiType;
    private GameObject player;
    [SerializeField] private int moveSpeed;
    public bool playerSpotted;
    public int damage;

    //moving
    [SerializeField] private GameObject[] points;
    [SerializeField] private int goal;
    private int direction;


    /// <summary>
    /// sets the ai type, assigns basic variables, and makes sure the enemy is alive
    /// </summary>
    void Start()
    {
        alive = true;
        cam = FindObjectOfType<CameraMarker>().gameObject;
        player = FindObjectOfType<PlayerController>().gameObject;
        gm = FindObjectOfType<GameManager>();
        damage = 10;
    }

    /// <summary>
    /// handles random enemy movement
    /// </summary>
    private void FixedUpdate()
    {
        float step = moveSpeed * Time.deltaTime;

        if (playerSpotted == false)
        {
            if (Vector3.Distance(transform.position, points[goal].transform.position) < 0.1f)
            {
                goal ++;
                if (goal == points.Length)
                {
                    goal = 1;
                }
            }

            transform.position = Vector3.MoveTowards(this.gameObject.transform.position, 
                points[goal].transform.position, step);
            transform.LookAt(points[goal].transform.position);
        }
    }

    /// <summary>
    /// handles ai response, among other functions described below
    /// </summary>
    void Update()
    {
        //keeps healthbar updated with health and maintains it looking at the camera
        if (alive)
        {
            healthBar.value = health;
        }
        healthBar.transform.LookAt(cam.transform.position);

        //handles death and enemies disappearing after 10 seconds
        if (health <= 0 && alive == true)
        {
            Die();
        }
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
        }
        if(deathTimer < 0)
        {
            Destroy(this.gameObject);
        }

        //AI
        /*
            all ai will wander aimlessly until the player is within viewing range
        0 - no ai (for debugging and testing)
        1 - attacking type, will move towards the player 
            when they touch the player, they will damage the player
        2 - fleeing type, will move away from the player
            nothing happens if they touch the player
        */

        //type 1
        if(aiType == 1 && alive && playerSpotted)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.LookAt(player.transform.position);
        }

        //type 2
        if(aiType == 2 && alive && playerSpotted)
        {
            Vector3 direction = transform.position - player.transform.position;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,
                transform.position + direction, step);
            transform.LookAt(transform.position + direction);
        }
    }

    /// <summary>
    /// handles enemy deaths and giving the player xp per kill
    /// </summary>
    private void Die()
    {
        alive = false;
        healthBar.gameObject.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        deathTimer = 10;
        if(aiType == 2)
        {
            gm.sm.xp += 10;
            gm.killCount++;
        }
    }

    /// <summary>
    /// uses the viewbox around the enemy to detect if the player is in sight range
    /// </summary>
    /// <param name="collider">colliding object, only looks for player tag</param
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Player")
        {
            playerSpotted = true;
        }
    }

    /// <summary>
    /// will make attacking types lose sight of the player if the player leaves their vision
    /// </summary>
    /// <param name="collision">colliding object, only looks for player tag</param>
    private void OnTriggerExit(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerSpotted = false;
        }
    }

    /// <summary>
    /// damages the player if the ai is an attacking type and alive
    /// </summary>
    /// <param name="collision">colliding object, only looks for player tag</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" && alive &&  aiType == 1)
        {
            player.GetComponent<PlayerController>().health -= damage;
        }
    }
}
