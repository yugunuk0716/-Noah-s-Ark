using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopup : Popup
{
    public Button continueBtn;

    void Start()
    {
        continueBtn.onClick.AddListener(() => PopupManager.instance.ClosePopup());
    }

}
