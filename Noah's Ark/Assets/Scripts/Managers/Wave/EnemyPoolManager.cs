using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoSingleton<EnemyPoolManager>
{
    public List<AIMove> enemyList;

    private void Awake()
    {
        enemyList = new List<AIMove>();
    }
    // TODO : 적 스폰 기능, Pool, type[], delay 만큼 넣어줘야 함

    /// <summary>
    /// 적을 스폰합니다.
    /// </summary>
    /// <param name="type">스폰할 적의 타입</param>
    public void Spawn(EnemyType type)
    {

        //enemyList.Add()
    }
}
