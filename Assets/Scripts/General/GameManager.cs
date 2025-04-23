/*****************************************************************************
// File Name : GameManager.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles all general game data and systems, including 
    lives, gui, winning and losing, and wave management.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //general info
    public SaveManager sm;
    public int lives;
    public int wave;

    public PlayerController pc;

    //player and the spawnpoint, used for respawning
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnpoint;

    //the goal of each level rests here, both of these must be equal to advance
    public int enemyCount;
    public int killCount;

    //upgrades
    public GameObject levelUpScreen;
    public int speed;
    public int strength;
    public int health;
    public TMP_Text speedAmount;
    public TMP_Text strengthAmount;
    public TMP_Text healthAmount;

    //win
    [SerializeField] private GameObject winScreen;

    //lose
    [SerializeField] private GameObject loseScreen;

    //gui
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text xpText;
    [SerializeField] private GameObject wave1Complete;
    [SerializeField] private GameObject wave2Complete;

    /// <summary>
    /// prepares variables for gameplay, along with the start menu
    /// </summary>
    void Start()
    {
        sm = FindObjectOfType<SaveManager>();
        lives = 3;
        wave = 1;
        pc = FindObjectOfType<PlayerController>();
        Time.timeScale = 0;
        //this is so picked up objects dont collide with the player
        Physics.IgnoreLayerCollision(6, 7, true);
        //Starts SMStart on reload
        if(sm.load > 0)
        {
            StartCoroutine(sm.SMStart());
        }
    }

    /// <summary>
    /// handles leveling up, gui maintainence, and the win condition
    /// </summary>
    private void Update()
    {
        healthBar.maxValue = pc.maxHealth;
        healthBar.value = pc.health;
        xpText.text = "XP: " + sm.xp;
        if(killCount == enemyCount && enemyCount > 1 && wave < 3)
        {
            StartCoroutine(WaveAdvance());
        }
        if (killCount == enemyCount && enemyCount > 1 && wave == 3)
        {
            Win();
        }
    }

    /// <summary>
    /// Handles beating a wave
    /// </summary>
    /// <returns></returns>
    IEnumerator WaveAdvance()
    {
        if (wave == 2)
        {
            wave2Complete.SetActive(true);
            killCount = 0;
            enemyCount = 0;
            yield return new WaitForSeconds(5);
            wave2Complete.SetActive(false);
            wave++;
        }
        if (wave == 1)
        {
            wave1Complete.SetActive(true);
            killCount = 0;
            enemyCount = 0;
            yield return new WaitForSeconds(5);
            wave1Complete.SetActive(false);
            wave++;
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
