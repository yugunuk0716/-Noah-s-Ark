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
    /// 스폰할 적 Type
    /// </summary>
    public EnemyType type;


    public EnemySpawnVO(float time, EnemyType type)
    {
        this.time = time;
        this.type = type;
    }
}
