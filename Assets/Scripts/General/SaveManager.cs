/*****************************************************************************
// File Name : SaveManager.cs
// Author : Brody Blanchard
// Creation Date : 4/16/2024
//
// Brief Description : Handles saving over upgrades when restarting the level
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //general info
    public int lives;
    public int level;
    public int wave;
    public int load;
    public GameManager gm;
    public bool synced;

    //determines when to level up and the gui
    public int xp;
    public int xpCeiling;

    //upgrades
    public int speed;
    public int strength;
    public int health;

    /// <summary>
    /// prepares variables for gameplay, along with the start menu
    /// </summary>
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        xp = 0;
        level = 1;
        xpCeiling = 10;
        speed = 0;
        strength = 0;
        health = 0;
        load = 0;
        StartCoroutine(SMStart());
    }

    public IEnumerator SMStart()
    {
        SMSync();
        print("fuck");

        yield return new WaitForSeconds(0.5f);

        SMAssign();
        print("shit");
    }

    public void SMSync()
    {
        gm = FindObjectOfType<GameManager>();
        gm.speed = speed;
        gm.strength = strength;
        gm.health = health;
    }

    public void SMAssign()
    {
        gm.pc.throwStrength = 12 + (strength * 3);
        gm.pc.moveSpeed = 5 + (speed * 0.25f);
        gm.pc.health = (Mathf.RoundToInt(gm.pc.health * (1 + (health * 0.3f))));
        gm.pc.maxHealth = (Mathf.RoundToInt(gm.pc.maxHealth * (1 + (health * 0.3f))));
        gm.strengthAmount.text = "" + (strength);
        gm.speedAmount.text = "" + (speed);
        gm.healthAmount.text = "" + (health);
        load++;
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
            xpCeiling = 10 * level;
            Time.timeScale = 0;
            gm.levelUpScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
