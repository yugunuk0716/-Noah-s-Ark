using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyManager : MonoSingleton<ActiveEnemyManager>
{
    // 맵에 스폰되어있는 모든 적을 가짐
    private List<GameObject> activeEnemyList = new List<GameObject>();

    // 매번 GetComponent() 하기는 조금 그래서
    private List<IMoveManagement> activeEnemyMoveManagementList = new List<IMoveManagement>();

    /// <summary>
    /// 스테이지에 스폰되어있는 적 숫자
    /// </summary>
    public int EnemyCount { 
        get {
            return activeEnemyList.Count;
        }
    }
    public enum SearchType // 검색 타입
    {
        FRONT,
        BACK,
        CLOSEST
    }


    /// <summary>
    /// 적을 스폰합니다.
    /// </summary>
    /// <param name="enemy"></param>
    public void Spawn(GameObject enemy)
    {
        enemy.SetActive(true);

        activeEnemyList.Add(enemy);
        activeEnemyMoveManagementList.Add(enemy.GetComponent<IMoveManagement>());
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
        enemy.SetActive(false);
        activeEnemyMoveManagementList.Remove(enemy.GetComponent<IMoveManagement>());
        activeEnemyList.Remove(enemy);
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
