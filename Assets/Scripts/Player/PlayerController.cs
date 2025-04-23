/*****************************************************************************
// File Name : PlayerController.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles player functions, controls, health, makes 
    upgrades work, and handles picking up and throwing objects.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    //General
    private GameManager gm;
    private GameObject player;
    [SerializeField] private GameObject center;
    private Rigidbody rb;
    public int health;
    public int maxHealth;
    public bool playing;

    //Movement
    [SerializeField] private PlayerInput pInput;
    public float moveSpeed;
    private Vector3 movement;

    //Rotation
    [SerializeField] private float sensitivity;
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject cam;
    [SerializeField] private Vector2 pRotate;

    //Pick Ups
    public bool canPickup;
    public GameObject possiblePickUp;
    private GameObject pickUpObject;
    [SerializeField] private float heldWeight;

    //Throw
    public float throwStrength;

    //Pause
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject GUI;

    /// <summary>
    /// prepares the player's stats for gameplay, defines basic variables
    /// </summary>
    private void Start()
    {
        player = this.gameObject;
        pInput.currentActionMap.Enable();
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
        maxHealth = 100;
        health = 100;
        throwStrength = 12;
        moveSpeed = 5;
        playing = false;
    }

    /// <summary>
    /// handles movement, camera rotation, and executes player death
    /// </summary>
    private void Update()
    {
        if(movement.x != 0)
        {
            transform.position += (center.transform.right * Time.deltaTime * movement.x) * (1 - heldWeight);
        }
        if(movement.z != 0)
        {
            transform.position += (center.transform.forward * Time.deltaTime * movement.z) * (1 - heldWeight);
        }

        transform.Rotate(0, pRotate.y, 0);

        if(health <= 0)
        {
            gm.Die();
        }
    }

    /// <summary>
    /// gives direction of movement, which is handled above
    /// </summary>
    /// <param name="direction">direction inputted</param>
    private void OnMovement(InputValue direction)
    {
        Vector2 moveDirection = direction.Get<Vector2>();
        movement.x = moveDirection.y * moveSpeed * -1;
        movement.z = moveDirection.x * moveSpeed;
    }

    /// <summary>
    /// handles picking up objects, which adds them as a child to this gameObject and increases held weight
    /// </summary>
    private void OnPickup()
    {
        if (possiblePickUp != null && pickUpObject == null)
        {
            pickUpObject = possiblePickUp;
            pickUpObject.layer = 6;
            pickUpObject.transform.parent = transform;
            pickUpObject.transform.position = front.transform.position;
            pickUpObject.transform.rotation = front.transform.rotation;
            pickUpObject.GetComponent<Rigidbody>().isKinematic = true;
            heldWeight = (pickUpObject.GetComponent<PickupController>().weight / 10) - (0.025f * (gm.strength - 1));
            if(heldWeight < 0)
            {
                heldWeight = 0;
            }
        }
    }

    /// <summary>
    /// throws the held item, if there is a held item, and clears all script side connections with it
    /// </summary>
    private void OnThrow()
    {
        if(pickUpObject != null)
        {
            pickUpObject.GetComponent<Rigidbody>().isKinematic = false;
            pickUpObject.transform.parent = null;
            pickUpObject.GetComponent<Rigidbody>().velocity = center.transform.right * -1 * 
                throwStrength + (center.transform.up * 3);
            pickUpObject.GetComponent<PickupController>().Throw(throwStrength);
            pickUpObject.GetComponent<PickupController>().flying = true;
            pickUpObject.layer = 0;
            pickUpObject = null;
            heldWeight = 0;
        }
    }

    private void OnMouse(InputValue pos)
    {
        if (playing)
        {
            Vector2 position = pos.Get<Vector2>();
            pRotate.x = position.y * sensitivity * -1;
            pRotate.y = position.x * sensitivity;
        }
    }

    /// <summary>
    /// Opens and closes pause menu
    /// </summary>
    private void OnPause()
    {
        if(pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            GUI.SetActive(true);
            playing = true;
            Cursor.lockState = CursorLockMode.Locked;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            GUI.SetActive(false);
            playing = false;
            Cursor.lockState = CursorLockMode.None;
            pRotate.x = 0;
            pRotate.y = 0;
        }
    }

    /// <summary>
    /// CHEAT
    /// Adds 10 xp on = press
    /// </summary>
    private void OnAddXP()
    {
        gm.sm.xp += 10;
    }

    /// <summary>
    /// CHEAT
    /// Adds one level on ] press
    /// </summary>
    private void OnAddLevel()
    {
        gm.sm.xp = gm.sm.xpCeiling;
    }
}
