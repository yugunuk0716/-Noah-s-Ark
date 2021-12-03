/// <summary>
/// 스폰될 시간과 타입 저장용
/// </summary>
[System.Serializable]
public class EnemySpawnVO
{
    /// <summary>
    /// 스폰될 시간<br/>0 ~ n 기준
    /// </summary>
    public float time;

    /// <summary>
    /// 스폰할 적 데이터
    /// </summary>
    public SpawnAmountVO spawn;


    public EnemySpawnVO(float time, SpawnAmountVO spawn)
    {
        this.time = time;
        this.spawn = spawn;
    }
}
