/*****************************************************************************
// File Name : MainMenu.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Handles main menu functions
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //this game object
    [SerializeField] private GameObject mainMenu;

    //other screen menus
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject hTPScreen;

    /// <summary>
    /// starts the game when the start button is pressed
    /// </summary>
    public void StartButton()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<PlayerController>().playing = true;
    }

    /// <summary>
    /// Opens the credits (unfinished)
    /// </summary>
    public void CreditsButton()
    {
        creditsScreen.SetActive(true);
    }

    /// <summary>
    /// Opens the how to play menu
    /// </summary>
    public void HowToPlayButton()
    {
        hTPScreen.SetActive(true);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }
}
