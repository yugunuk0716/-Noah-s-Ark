using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Button waveStartButton;
    public Text moneyText;
    public Text waveText;
    public Image hpBar;
    public Image mpBar;


    void Start()
    {
        waveStartButton.onClick.AddListener(() => 
        {
            WaveManager.Instance.StartNewWave();
        });
        SetUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PopupManager.instance.OpenPopup("menu");
        }
        
    }

    public void SetUI() 
    {
        //(int a, int b) = WaveManager.Instance.GetWaveData();
        //waveText.text = string.Format("Wave {0} / {1}", a, b);

        mpBar.fillAmount = (float)TurretManager.Instance.GetCurrentTurret().curMp / TurretManager.Instance.GetCurrentTurret().maxMp;


    }
}
