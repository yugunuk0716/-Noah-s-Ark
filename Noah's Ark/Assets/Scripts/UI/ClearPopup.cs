using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPopup : Popup
{
    public Button nextSceneButton;
    public Button goStageSelectButton;
    public Button retryButton;

    Fade fade;

    private void Start()
    {
        fade = GetComponentInParent<Fade>();
    }

    private void OnClickNextStageButton() 
    {
        fade.FadeIn();
    }

    private void OnClickGoStageSelectButton() 
    {
        fade.FadeIn();
    }

    private void OnClickRetryButton() 
    {
        fade.FadeIn();
    }
}
