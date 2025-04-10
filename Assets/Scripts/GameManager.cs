/*****************************************************************************
// File Name : GameManager.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles all general game data and systems, including xp, 
    lives, gui, level up data, winning and losing.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //general info
    public int lives;
    public int level;

    //determines when to level up and the gui
    public int xp;
    private int xpCeiling;
    [SerializeField] private TMP_Text xpText;

    private PlayerController pc;

    //player and the spawnpoint, used for respawning
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnpoint;

    //the goal of each level rests here, both of these must be equal to advance
    public int enemyCount;
    public int killCount;

    //upgrades
    [SerializeField] private GameObject levelUpScreen;
    public int speed;
    public int strength;
    public int health;

    //win
    [SerializeField] private GameObject winScreen;

    //lose
    [SerializeField] private GameObject loseScreen;

    //gui
    [SerializeField] private Slider healthBar;

    /// <summary>
    /// prepares variables for gameplay, along with the start menu
    /// </summary>
    void Start()
    {
        xp = 0;
        lives = 3;
        level = 1;
        xpCeiling = 50;
        pc = FindObjectOfType<PlayerController>();
        Time.timeScale = 0;
        //this is so picked up objects dont collide with the player
        Physics.IgnoreLayerCollision(6, 7, true);
        speed = 1;
        strength = 1;
        health = 1;
    }

    /// <summary>
    /// handles leveling up, gui maintainence, and the win condition
    /// </summary>
    private void Update()
    {
        if(xp >= xpCeiling)
        {
            xp -= xpCeiling;
            level += 1;
            xpCeiling = 25 * level;
            Time.timeScale = 0;
            levelUpScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        healthBar.maxValue = pc.maxHealth;
        healthBar.value = pc.health;
        xpText.text = "XP: " + xp;
        if(killCount == enemyCount && enemyCount > 1)
        {
            Win();
        }
    }

    /// <summary>
    /// executes when the player's health reaches 0. will also execute losing if lives reach 0, otherwise will respawn
    /// the player
    /// </summary>
    public void Die()
    {
        lives--;
        pc.health = pc.maxHealth;
        player.transform.position = spawnpoint.transform.position;
        if(lives == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            loseScreen.SetActive(true);
        }
    }

    /// <summary>
    /// handles winning, when the kill count is equal to the amount of spawned enemies
    /// </summary>
    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }
}
