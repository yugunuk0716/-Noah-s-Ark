using FORGE3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTowerPopup : Popup
{
    public Button closeBtn;
    public Button createBtn;

    private void Start()
    {
        closeBtn.onClick.AddListener(() =>PopupManager.instance.ClosePopup());
        createBtn.onClick.AddListener(() => {
            F3DTurret a =  TowerSpawner.instance.CreateTower(TowerSpawner.instance.GetTowerSpawnPos().parent);
            a.transform.localScale = new Vector3(0.25f, 1, 0.25f);
            PopupManager.instance.ClosePopup();
            TowerSpawner.instance.Init();
        });
    }
}
