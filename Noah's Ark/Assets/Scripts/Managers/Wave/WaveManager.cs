using System.IO;
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

    private bool _doNotSpawn = false;
    public bool DoNotSpawn { 
        get { return _doNotSpawn; }
        set { _doNotSpawn = value; }
    }

    public bool IsSpawnFinished { get; private set; } = true;
    public bool IsWaveFinished { get; private set; } = true;
    public bool NomoreWaveLeft { get; private set; } = false;
    public bool StageCleared { get; private set; } = false;

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
            string waveJson = JsonFileManager.Read(waveName + ++waveIndex, Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Waves"), true);
            Debug.Log(waveJson);

            if(waveJson == null) break;
            waves.Add(JsonUtility.FromJson<WaveVO>(waveJson));
        }
        waveIndex = 0;

        OnWaveStarted   += () => { StartCoroutine(StartWave()); };

        OnWaveCompleted += () => {
            Debug.Log("OnWaveCompleted");
            ++waveIndex;
            midWaveIndex = 0;
        };
        
        OnStageCompleted += () => {
            Debug.Log("OnStageCompleted");
            waveIndex = 0;
            midWaveIndex = 0;
            // IsWaveFinished = true;
        };

    }

    public bool FirstWave()
    {
        return (waveIndex == 0 && midWaveIndex == 0);
    }

    #region 웨이브
    /// <summary>
    /// 웨이브를 시작합니다.
    /// </summary>
    public void StartNewWave()
    {
        Debug.Log("Called StartWave:" + IsWaveFinished);
        if(IsWaveFinished && !StageCleared)
        {
            IsWaveFinished = false;
            IsSpawnFinished = false;
            OnWaveStarted();
        }
    }
    IEnumerator StartWave()
    {
        if(waveIndex >= waves.Count) yield break;
        time = 0.0f;

        while (true)
        {
            if(DoNotSpawn) continue;

            time += Time.deltaTime;
            if (time >= waves[waveIndex][midWaveIndex].time)
            {
                StartCoroutine(SpawnEnemy(waves[waveIndex][midWaveIndex++].spawn));

                if(midWaveIndex >= waves[waveIndex].spawnData.Length) break;
            }
            yield return null;
        }

        if(waveIndex >= waves.Count) NomoreWaveLeft = true;

        IsSpawnFinished = true;
    }

    /// <summary>
    /// 스테이지를 끝남으로 설정합니다.
    /// </summary>
    public void SetStageCompleted()
    {
        OnStageCompleted();
        StageCleared = true;
        Debug.LogWarning("Clear!");
    }

    /// <summary>
    /// 웨이브를 끝남으로 설정합니다.
    /// </summary>
    public void SetWaveCompleted()
    {
        IsWaveFinished = true;
        OnWaveCompleted();
        Debug.LogWarning("Done!");
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
        return (waveIndex + 1, waves.Count);
    }
}
