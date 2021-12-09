using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManganager : MonoSingleton<UIManganager>
{
    
    public Button waveStartButton;
    public Image hpBar;
    public Image mpBar;
    public Text moneyText;
    public Text waveText;


    [Header("MiniMap")]
    public Camera minimapCamera;
    public RawImage minimapImage;
    public RenderTexture miniMapTexture;


    void Start()
    {
        waveStartButton.onClick.AddListener(() => 
        {
            WaveManager.Instance.StartNewWave();
        });
        SetText();

        WaveManager.Instance.OnStageCompleted += MinimapFocus;
        WaveManager.Instance.OnWaveCompleted += MinimapFocus;
        WaveManager.Instance.OnWaveStarted += MinimapFocus;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PopupManager.instance.OpenPopup("menu");
        }
       

    }


    public void MinimapFocus()  //웨이브 끝났을 때
    {
        minimapCamera.targetTexture = null;
        minimapImage.gameObject.SetActive(false);
    }

    public void MinimapDefocus() //웨이브 시작할 때
    {
        minimapCamera.targetTexture = miniMapTexture;
        minimapImage.gameObject.SetActive(true);
    }

    public void SetText() 
    {
        //(int a, int b) = WaveManager.Instance.GetWaveData();
        //waveText.text = string.Format("Wave {0} / {1}", a, b);

    }
}
