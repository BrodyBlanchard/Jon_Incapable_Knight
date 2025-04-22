using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private GameObject first;
    [SerializeField] private GameObject second;
    [SerializeField] private GameObject third;
    [SerializeField] private GameObject fourth;
    [SerializeField] private GameObject fifth;
    private int currentTutorial;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject nextButton;

    private void Start()
    {
        currentTutorial = 1;
    }

    public void ReturnButton()
    {
        this.gameObject.SetActive(false);
    }

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
