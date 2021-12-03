using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerPopup : Popup
{
    public Button closeBtn;

    private void Start()
    {
        closeBtn.onClick.AddListener(() => PopupManager.instance.ClosePopup());
    }
}
