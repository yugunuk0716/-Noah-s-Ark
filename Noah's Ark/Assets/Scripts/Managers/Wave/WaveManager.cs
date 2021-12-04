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
    private WaveVO waves;
    private float time = 0;
    private int idx = 0;


    private void Awake()
    {
        int index = 0;

        while(index < waveCount)
        {
            string waveJson = JsonFileManager.Read(waveName + ++index);
            Debug.Log(waveJson);

            if(waveJson == null) break;
            waves = JsonUtility.FromJson<WaveVO>(waveJson);
        }
    }

    private void Start()
    {
        foreach(var item in waves.spawnData)
        {
            Debug.Log(item.time);
        }

        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        while (true)
        {
            time += Time.deltaTime;
            if (time >= waves[idx].time)
            {
                StartCoroutine(SpawnEnemy(waves[idx++].spawn));

                if(idx >= waves.spawnData.Length) break;
            }
            yield return null;
        }


    }

    IEnumerator SpawnEnemy(SpawnAmountVO spawnData)
    {
        WaitForSeconds wait = new WaitForSeconds(spawnData.delay);

        //EnemyPoolManager.Instance.Spawn(spawnData);
        for (int i = 0; i < spawnData.spawnList.Count; ++i)
        {
            yield return wait;
            EnemyPoolManager.Instance.Spawn(spawnData.spawnList[i]);
        }
    }
}
