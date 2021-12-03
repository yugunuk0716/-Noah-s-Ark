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
    public EnemySpawnVO[] spawnData = new EnemySpawnVO[0];

    [Header("되도록이면 이름은 건들지 말아줘요.")]
    public string waveName = "wave";
    [Header("웨이브 단계")]
    public int waveNumber = 1;

    private void Start()
    {
        string waveJson = "";

        for (int i = 0; i < spawnData.Length; ++i)
        {
            waveJson += JsonUtility.ToJson(spawnData[i]);
        }

        JsonFileManager.Write(waveName + waveNumber.ToString(), waveJson);
    }
}
