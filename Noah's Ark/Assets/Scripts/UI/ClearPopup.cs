using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearPopup : Popup
{
    // public Button nextStageButton;
    public Button goStageSelectButton;
    public Button retryButton;

    Fade fade;

    private void Start()
    {
        fade = GetComponentInParent<Fade>();

        // nextStageButton.onClick.AddListener(OnClickNextStageButton);
        goStageSelectButton.onClick.AddListener(OnClickGoStageSelectButton);
        retryButton.onClick.AddListener(OnClickRetryButton);
    }

    // private void OnClickNextStageButton() 
    // {
    //     fade.FadeIn();
    //     StageManager.instance.Stage++;
    //     SceneManager.LoadScene("InGame");
    // }

    private void OnClickGoStageSelectButton() 
    {
        fade.FadeIn();
        SceneManager.LoadScene("StageSelect");
    }

    private void OnClickRetryButton() 
    {
        fade.FadeIn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
