/// <summary>
/// 스테이지 웨이브 저장용
/// </summary>
[System.Serializable]
public class WaveVO
{
    /// <summary>
    /// 스폰 데이터
    /// </summary>
    public EnemySpawnVO[] spawnData;

    public WaveVO(EnemySpawnVO[] spawnData)
    {
        this.spawnData = spawnData;
    }

    public EnemySpawnVO this[int index]
    {
        get {
            return spawnData[index];
        }
        set {
            spawnData[index] = value;
        }
    }
}
