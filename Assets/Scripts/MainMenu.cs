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

    /// <summary>
    /// starts the game when the start button is pressed
    /// </summary>
    public void StartButton()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
