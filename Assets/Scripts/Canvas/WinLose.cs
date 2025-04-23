/*****************************************************************************
// File Name : WinLose.cs
// Author : Brody Blanchard
// Creation Date : 3/31/2024
//
// Brief Description : Functions used by the win and lose screens.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
