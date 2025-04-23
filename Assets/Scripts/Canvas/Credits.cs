/*****************************************************************************
// File Name : Credits.cs
// Author : Brody Blanchard
// Creation Date : 4/18/2024
//
// Brief Description : Function used by the credits screen
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    /// <summary>
    /// Returns to the main menu
    /// </summary>
    public void ReturnButton()
    {
        this.gameObject.SetActive(false);
    }
}
