using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWaveUI : MonoBehaviour
{
    [SerializeField] private Button startButton = null;

    private void Start()
    {
        if (startButton == null)
        {
            Debug.LogError($"StartWaveUI > Button is null.\r\nAt object: {this.gameObject.name}");
            this.enabled = false;
            return;
        }


        startButton.onClick.AddListener(() => {
            WaveManager.Instance.StartNewWave();
        });
    }




}
