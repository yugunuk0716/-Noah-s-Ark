using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [Header("웨이브 파일 이름")]
    [SerializeField] private string waveName = "wave";

    [Header("불러올 웨이브 수")]
    [SerializeField] private int waveCount = 0; // 이것보다 적으면 전부 불러올 수 있음

    public Difficulty difficulty = Difficulty.NORMAL; // 아직은 적용 안함

    /// <summary>
    /// 모든 웨이브들을 가짐
    /// </summary>
    private List<EnemySpawnVO> waves;

    
    private void Awake()
    {
        int index = 0;

        while(index < waveCount)
        {
            string waveJson = JsonFileManager.Read(waveName + ++index);

            if(waveJson == null) break;
            waves.Add(JsonUtility.FromJson<EnemySpawnVO>(waveJson));
        }
    }
    
    private void Update()
    {
        
    }

}
