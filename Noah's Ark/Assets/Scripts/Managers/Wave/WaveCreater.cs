using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

1. time

2. key: time value: what to spawn

3. key: what to spawn value: spawn count and delay
*/

public class WaveCreater : MonoBehaviour
{
    [Header("time: 이전 웨이브로부터 지난 시간. 적이 다 스폰되고 나서의 시간이 기준")]
    public EnemySpawnVO[] spawnData = new EnemySpawnVO[0];

    [Header("되도록이면 이름은 건들지 말아줘요.")]
    public string waveName = "wave";
    [Header("웨이브 단계")]
    public int waveNumber = 1;

    private float time = 0;

    private void Start()
    {
        string waveJson = "";

        for (int i = 0; i < spawnData.Length; ++i)
        {
            int beforeIndex = i - 1;
            
            if (i > 0) // 스폰 시간 
            {
                time += spawnData[beforeIndex].time;
                for (int j = 0; j < spawnData[beforeIndex].spawn.spawnList.Count; ++j)
                {
                    time += spawnData[beforeIndex].spawn.delay;
                }
            }

            spawnData[i].time = time;
            waveJson += JsonUtility.ToJson(spawnData[i]);
        }

        JsonFileManager.Write(waveName + waveNumber.ToString(), waveJson);
    }
}
