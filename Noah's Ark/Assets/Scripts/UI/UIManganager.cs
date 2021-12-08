using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManganager : MonoSingleton<UIManganager>
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
        SetText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PopupManager.instance.OpenPopup("menu");
        }
        
    }

    public void SetText() 
    {
        //(int a, int b) = WaveManager.Instance.GetWaveData();
        //waveText.text = string.Format("Wave {0} / {1}", a, b);

    }
}
