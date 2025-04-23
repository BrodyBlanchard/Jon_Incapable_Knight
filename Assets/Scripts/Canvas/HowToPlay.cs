/*****************************************************************************
// File Name : HowToPlay.cs
// Author : Brody Blanchard
// Creation Date : 4/17/2024
//
// Brief Description : Handles all the functions in the how to play screen from
    the main menu
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    //Tutorial screens
    [SerializeField] private GameObject first;
    [SerializeField] private GameObject second;
    [SerializeField] private GameObject third;
    [SerializeField] private GameObject fourth;
    [SerializeField] private GameObject fifth;

    private int currentTutorial;
    
    //Navigation buttons
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject nextButton;

    /// <summary>
    /// Ensures that we start on the first slide
    /// </summary>
    private void Start()
    {
        currentTutorial = 1;
    }

    /// <summary>
    /// Returns to the main menu
    /// </summary>
    public void ReturnButton()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Will change the visible tutorial to the previous tutorial
    /// </summary>
    public void PreviousButton()
    {
        if (currentTutorial > 1)
        {
            currentTutorial--;
        }

        switch (currentTutorial)
        {
            case 1:
                previousButton.SetActive(false);
                second.SetActive(false);
                break;
            case 2:
                third.SetActive(false);
                break;
            case 3:
                fourth.SetActive(false);
                break;
            case 4:
                nextButton.SetActive(true);
                fifth.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Will change the visible tutorial to the next tutorial
    /// </summary>
    public void NextButton()
    {
        if(currentTutorial < 5)
        {
            currentTutorial++;
        }

        switch (currentTutorial)
        {
            case 2:
                previousButton.SetActive(true);
                second.SetActive(true);
                break;
            case 3:
                third.SetActive(true);
                break;
            case 4:
                fourth.SetActive(true);
                break;
            case 5:
                nextButton.SetActive(false);
                fifth.SetActive(true);
                break;
        }
    }

}
