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
    public Camera miniMapCamera;
    public RawImage miniMapImage;
    public RenderTexture miniMapTexture;


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


    public void MiniMaptoMain()  //웨이브 끝났을 때
    {
        miniMapCamera.targetTexture = null;
        miniMapImage.gameObject.SetActive(false);
    }

    public void MiniMapReset() //웨이브 시작할 때
    {
        miniMapCamera.targetTexture = miniMapTexture;
        miniMapImage.gameObject.SetActive(true);
    }

    public void SetText() 
    {
        //(int a, int b) = WaveManager.Instance.GetWaveData();
        //waveText.text = string.Format("Wave {0} / {1}", a, b);

    }
}
