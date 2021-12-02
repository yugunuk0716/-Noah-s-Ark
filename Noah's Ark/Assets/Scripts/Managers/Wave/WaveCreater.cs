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

    private void Start()
    {
        JsonFileManager.Write(Time.deltaTime.ToString(), JsonUtility.ToJson(spawnData));
    }
}
