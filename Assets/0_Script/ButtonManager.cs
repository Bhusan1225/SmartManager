using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button nextButtons;
    [SerializeField] Button quitButtons;

    private void Start()
    {
        if (nextButtons != null)
        {
            nextButtons.onClick.AddListener(OnNextButtonClicked);
        }
        else
        {
            Debug.LogError("Next button is not assigned in the inspector.");
        }
        if (quitButtons != null)
        {
            quitButtons.onClick.AddListener(OnQuitButtonClicked);
        }
        else
        {
            Debug.LogError("Quantity button is not assigned in the inspector.");
        }
    }

    private void OnQuitButtonClicked()
    {
        SceneService.Instance.LoadScene(0); //Lobby 
    }

    private void OnNextButtonClicked()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneService.Instance.LoadScene(nextSceneIndex); //Load next scene
    }
}
