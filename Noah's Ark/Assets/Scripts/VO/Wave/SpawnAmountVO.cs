using System;
using System.Collections.Generic;

/// <summary>
/// 스폰 데이터
/// </summary>
[Serializable]
public class SpawnAmountVO
{
    /// <summary>
    /// 스폰 딜레이
    /// </summary>
    public float delay;

    /// <summary>
    /// 스폰 리스트
    /// </summary>
    public List<EnemyType> spawnList;

    public SpawnAmountVO(float delay, List<EnemyType> spawnList)
    {
        this.delay = delay;
        this.spawnList = spawnList;
    }
}
