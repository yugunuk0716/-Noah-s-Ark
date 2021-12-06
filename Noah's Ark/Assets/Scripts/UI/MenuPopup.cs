using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopup : Popup
{
    public Button closeBtn;

    void Start()
    {
        closeBtn.onClick.AddListener(() => PopupManager.instance.ClosePopup());
    }

}
