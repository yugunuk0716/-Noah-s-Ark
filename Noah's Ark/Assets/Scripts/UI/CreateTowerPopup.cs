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
            TurretManager.Instance.AddTurret(a);
            print("Create");
            a.transform.localScale = new Vector3(0.25f, 0.33f, 0.25f);
            TowerSpawner.instance.GroundStateChange();
            PopupManager.instance.ClosePopup();
        });
    }

    public override void Close()
    {
        base.Close();
        TowerSpawner.instance.Init();
    }
}
