using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyManager : MonoSingleton<ActiveEnemyManager>
{
    /// <summary>
    /// 적 스폰시 적 오브젝트를 인자로 전달하면서 호출됨
    /// </summary>
    public event System.Action<GameObject> OnSpawnEnemy;

    // 맵에 스폰되어있는 모든 적을 가짐
    private List<GameObject> activeEnemyList = new List<GameObject>();

    // 매번 GetComponent() 하기는 조금 그래서
    private List<IMoveManagement> activeEnemyMoveManagementList = new List<IMoveManagement>();

    /// <summary>
    /// 스테이지에 스폰되어있는 적 숫자
    /// </summary>
    public int EnemyCount 
    { 
        get 
        {
            return activeEnemyList.Count;
        }
    }
    public enum SearchType // 검색 타입
    {
        FRONT,
        BACK,
        CLOSEST
    }

    private void Start()
    {
        OnSpawnEnemy += (e) => { };
    }

    /// <summary>
    /// 적을 스폰합니다.
    /// </summary>
    /// <param name="enemy"></param>
    public void Spawn(GameObject enemy)
    {
        activeEnemyList.Add(enemy);
        activeEnemyMoveManagementList.Add(enemy.GetComponent<IMoveManagement>());

        OnSpawnEnemy(enemy);

        enemy.SetActive(true);
    }

    /// <summary>
    /// 활성화 상태인 모든 적을 가져옵니다.
    /// </summary>
    /// <returns>List of Enemy GameObject</returns>
    public List<GameObject> GetAllEnemy()
    {
        return activeEnemyList.FindAll(x => x.activeSelf);
    }

    /// <summary>
    /// 조건을 만족하는 적을 가져옵니다.
    /// </summary>
    /// <param name="search">검색 타입</param>
    /// <param name="position">제일 가까운 적을 찾는 경우, 검색을 요청한 오브젝트의 좌표</param>
    public GameObject GetEnemy(SearchType search, Vector3? position = null)
    {
        GameObject resultEnemy = null; // 제일 앞 오브젝트

        // 맨 앞의 적
        int maxDestIndex = int.MinValue;
        float minDist = float.MaxValue;

        // 맨 뒤의 적
        int minDestIndex = int.MaxValue;
        float maxDist = float.MinValue;

        // 제일 가까운 적
        float distanceWithEnemy = float.MaxValue;

        for (int i = 0; i < activeEnemyMoveManagementList.Count; ++i) // 목적지 인덱스
        {
            switch(search)
            {
                case SearchType.CLOSEST: // 제일 가까운 적
                    if(distanceWithEnemy > Vector3.Distance((Vector3)position, activeEnemyList[i].transform.position))
                    {
                        resultEnemy = activeEnemyList[i];
                    }
                    break;

                case SearchType.FRONT: // 맨 앞의 적
                    if (activeEnemyMoveManagementList[i].CurrentDestIdx >= maxDestIndex &&
                        activeEnemyMoveManagementList[i].GetRemainDistance() >= minDist)
                    {
                        resultEnemy = activeEnemyList[i];
                    }
                    break;

                case SearchType.BACK: // 맨 뒤의 적
                    if (activeEnemyMoveManagementList[i].CurrentDestIdx <= minDestIndex &&
                        activeEnemyMoveManagementList[i].GetRemainDistance() <= maxDist)
                    {
                        resultEnemy = activeEnemyList[i];
                    }
                    break;
            }
            
        }

        return resultEnemy;
    }

    /// <summary>
    /// 적을 관리 리스트에서 제거합니다.
    /// </summary>
    /// <param name="enemy">제거할 적</param>
    public void Remove(GameObject enemy)
    {
        enemy.GetComponent<AIMove>().ResetAI();
        activeEnemyMoveManagementList.Remove(enemy.GetComponent<IMoveManagement>());
        activeEnemyList.Remove(enemy);
        enemy.SetActive(false);

        CheckAllEnemyGone();
    }

    /// <summary>
    /// 모든 적 스폰이 완료됬고, 남은 적이 있는지
    /// </summary>
    private void CheckAllEnemyGone()
    {
        if(activeEnemyList.Count == 0)
        {
            if(WaveManager.Instance.NomoreWaveLeft)
                WaveManager.Instance.SetStageCompleted();
            if(WaveManager.Instance.IsSpawnFinished)
                WaveManager.Instance.SetWaveCompleted();
        }
    }


    /// <summary>
    /// 모든 적에게 행동을 실행합니다.<br/>
    /// </summary>
    /// <param name="action">delegate(GameObject). GameObject is Enemy's GameObject</param>
    public void ControlAllEnemy(System.Action<GameObject> action)
    {
        for (int i = 0; i < activeEnemyList.Count; ++i)
        {
            action(activeEnemyList[i]);
        }
    }
    
}
