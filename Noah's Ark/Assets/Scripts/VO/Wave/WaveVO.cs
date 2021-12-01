/// <summary>
/// 스테이지 웨이브 저장용
/// </summary>
[System.Serializable]
public class WaveVO
{
    /// <summary>
    /// 스테이지 인덱스
    /// </summary>
    public int index;

    /// <summary>
    /// 스폰 데이터
    /// </summary>
    public EnemySpawnVO[] spawnData;

    public WaveVO(int index, EnemySpawnVO[] spawnData)
    {
        this.index = index;
        this.spawnData = spawnData;
    }
}
