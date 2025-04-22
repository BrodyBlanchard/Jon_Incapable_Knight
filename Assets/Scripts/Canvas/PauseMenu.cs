/*****************************************************************************
// File Name : PauseMenu.cs
// Author : Brody Blanchard
// Creation Date : 4/16/2024
//
// Brief Description : Handles pause menu functions, such as resuming, restarting,
    and returning to the main menu
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject GUI;
    [SerializeField] private PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        GUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        pc.playing = true;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
        GUI.SetActive(true);
    }

    public void MainMenuButton()
    {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
        GUI.SetActive(true);
    }
}
