using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Button waveStartButton;
    public Text moneyText;
    public Text waveText;
    public Image lifeBar;


    void Start()
    {
        waveStartButton.onClick.AddListener(() => 
        {
            WaveManager.Instance.StartNewWave();
        });
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
        
    }
}
