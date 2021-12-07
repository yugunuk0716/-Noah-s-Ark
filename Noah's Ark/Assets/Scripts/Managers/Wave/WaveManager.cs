using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [Header("웨이브 파일 이름")]
    [SerializeField] private string waveName = "wave";

    [Header("불러올 웨이브 수")]
    [SerializeField] private int waveCount = 0; // 이것보다 적으면 전부 불러올 수 있음

#region Action
    /// <summary>
    /// 웨이브 하나가 끝나면 호출됩니다.
    /// </summary>
    public event System.Action OnWaveCompleted;

    /// <summary>
    /// 웨이브가 실행되면 호출됩니다.
    /// </summary>
    public event System.Action OnWaveStarted;

    /// <summary>
    /// 스테이지 클리어 시 호출됩니다.<br/>
    /// 모든 웨이브 클리어 시.
    /// </summary>
    public event System.Action OnStageCompleted;
    #endregion

    public bool IsWavePlaying { get; private set; } = false;

    public Difficulty difficulty = Difficulty.NORMAL; // 아직은 적용 안함

    /// <summary>
    /// 모든 웨이브들을 가짐
    /// </summary>
    private List<WaveVO> waves = new List<WaveVO>();
    private float time = 0; // 웨이브 지나고 시간
    private int waveIndex = 0; // 웨이브
    private int midWaveIndex = 0; // 웨이브 안 웨이브


    private void Awake()
    {
        while(waveIndex < waveCount)
        {
            string waveJson = JsonFileManager.Read(waveName + ++waveIndex);
            Debug.Log(waveJson);

            if(waveJson == null) break;
            waves.Add(JsonUtility.FromJson<WaveVO>(waveJson));
        }
        waveIndex = 0;

        OnWaveStarted   += () => { StartCoroutine(StartWave()); };
        OnWaveCompleted += () => { };
        OnStageCompleted += () => { };

    }

    #region 웨이브
    /// <summary>
    /// 웨이브를 시작합니다.
    /// </summary>
    public void StartNewWave()
    {
        if(IsWavePlaying) return;
        OnWaveStarted();
    }
    IEnumerator StartWave()
    {
        IsWavePlaying = true;
        time = 0.0f;

        while (true)
        {
            time += Time.deltaTime;
            if (time >= waves[waveIndex][midWaveIndex].time)
            {
                StartCoroutine(SpawnEnemy(waves[waveIndex][midWaveIndex++].spawn));

                if(midWaveIndex >= waves[waveIndex].spawnData.Length) break;
            }
            yield return null;
        }

        ++waveIndex;
        midWaveIndex = 0;
        IsWavePlaying = false;

        if(waveIndex >= waves.Count) OnStageCompleted();
        else OnWaveCompleted();
    }
    #endregion

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

    /// <summary>
    /// 웨이브 단계를 반환합니다.
    /// </summary>
    /// <returns>현재 웨이브 / 총 웨이브</returns>
    public (int, int) GetWaveData()
    {
        return (waveIndex, waves.Count);
    }
}
