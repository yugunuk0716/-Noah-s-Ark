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


    [SerializeField] private Button startButton = null;



    void Start()
    {
        waveStartButton.onClick.AddListener(() => 
        {
            WaveManager.Instance.StartNewWave();
        });
        SetText();

        WaveManager.Instance.OnWaveCompleted += MinimapFocus;
        WaveManager.Instance.OnStageCompleted += MinimapFocus;
        WaveManager.Instance.OnWaveStarted += MinimapDefocus;
        WaveManager.Instance.OnWaveStarted += SetText;

        if (startButton == null)
        {
            Debug.LogError($"StartWaveUI > Button is null.\r\nAt object: {this.gameObject.name}");
            this.enabled = false;
            return;
        }


        startButton.onClick.AddListener(() => {
            WaveManager.Instance.StartNewWave();
        });

        MinimapFocus();

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
        if (TurretManager.Instance.GetCurrentTurret() != null) 
        {
            TurretManager.Instance.GetCurrentTurret().isPlayer = false;
            
        }
        minimapImage.gameObject.SetActive(false);
        print("Focus");
    }

    public void MinimapDefocus() //웨이브 시작할 때
    {
        if (TurretManager.Instance.GetCurrentTurret() != null)
        {
            TurretManager.Instance.GetCurrentTurret().isPlayer = true;
            TurretManager.Instance.mainCam.transform.SetParent(TurretManager.Instance.GetCurrentTurret().camTrm);
        }
            
        minimapCamera.targetTexture = miniMapTexture;
        TurretManager.Instance.mainCam.transform.localPosition = Vector3.zero;
        TurretManager.Instance.mainCam.transform.localRotation = Quaternion.identity;
        minimapImage.gameObject.SetActive(true);
        print("Defocus");
    }

    public void SetText() 
    {
        (int a, int b) = WaveManager.Instance.GetWaveData();
        waveText.text = string.Format("Wave {0} / {1}", a, b);

    }
}
