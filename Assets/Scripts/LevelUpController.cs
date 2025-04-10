/*****************************************************************************
// File Name : LevelUpController.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles leveling up and the level up screen.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    private GameManager gm;
    private PlayerController pc;

    /// <summary>
    /// defines the basic variables
    /// </summary>
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pc = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// upgrades the strength stat when it is selected in the upgrades menu, then continues gameplay
    /// </summary>
    public void UStrength()
    {
        gm.strength += 1;
        pc.throwStrength = 12 + (gm.strength * 3);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// upgrades the speed stat when it is selected in the upgrades menu, then continues gameplay
    /// </summary>
    public void USpeed()
    {
        gm.speed += 1;
        pc.moveSpeed = 5 + (gm.speed * 0.25f);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// upgrades the health stat when it is selected in the upgrades menu, then continues gameplay
    /// </summary>
    public void UHealth()
    {
        gm.health += 1;
        pc.health = (Mathf.RoundToInt(pc.health * (1 + (gm.health * 0.3f))));
        pc.maxHealth = (Mathf.RoundToInt(pc.maxHealth * (1 + (gm.health * 0.3f))));
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
