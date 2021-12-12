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
            TowerSpawner.instance.CreateTower(TowerSpawner.instance.GetTowerSpawnPos().parent);
            TowerSpawner.instance.GroundStateChange();
            PopupManager.instance.ClosePopup();
        });
    }

    public override void Close()
    {
        TowerSpawner.instance.Init();
        base.Close();
    }
}
