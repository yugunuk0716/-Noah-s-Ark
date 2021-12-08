using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuPopup : Popup
{
    public Button continueBtn;
    public Button homeButton;
    public Button optionButton;
    public Button backButton;

    public Slider soundSlider;
    public Slider sfxSlider;

    public CanvasGroup menuWindow, optionWindow;


    void Start()
    {
        continueBtn.onClick.AddListener(() => PopupManager.instance.ClosePopup());
        optionButton.onClick.AddListener(() => OnClickOptionButton());
        homeButton.onClick.AddListener(() => OnClickHomeButton());
        backButton.onClick.AddListener(() => OnClickBackButton());
        soundSlider.onValueChanged.AddListener(a => 
        {
            SoundManager.Instance.ChangeBgmVolume(soundSlider.value);
        });
        sfxSlider.onValueChanged.AddListener(a =>
        {
            SoundManager.Instance.ChangeSfxVolume(sfxSlider.value);
        });

    }


    private void OnClickOptionButton() 
    {

        DOTween.To(() => menuWindow.alpha, value => menuWindow.alpha = value, 0, 0.8f).OnComplete(() =>
        {
            menuWindow.interactable = false;
            menuWindow.blocksRaycasts = false;
        });


        optionWindow.interactable = true;
        DOTween.To(() => optionWindow.alpha, value => optionWindow.alpha = value, 1, 0.8f).OnComplete(() =>
        {
            optionWindow.interactable = true;
            optionWindow.blocksRaycasts = true;
        });


    }

    private void OnClickBackButton() 
    {
        DOTween.To(() => optionWindow.alpha, value => optionWindow.alpha = value, 0, 0.8f).OnComplete(() =>
        {
            optionWindow.interactable = false;
            optionWindow.blocksRaycasts = false;
        });


        menuWindow.interactable = true;
        DOTween.To(() => menuWindow.alpha, value => menuWindow.alpha = value, 1, 0.8f).OnComplete(() =>
        {
            menuWindow.interactable = true;
            menuWindow.blocksRaycasts = true;
        });
    }

    private void OnClickHomeButton() 
    {
        //씬이동 넣어야 되구요
    }




}
