using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoSingleton<EnemyPoolManager>
{

    [Header("적 프리팹. 넣는 순서는 EnemyType 과 같아야 함")]
    [SerializeField] private GameObject[] enemyPrefabs = new GameObject[(int)EnemyType.END_OF_ENUM];

    [Header("AI 가 목적지로 가질 포지션")]
    public List<Transform> enemyDestList = new List<Transform>();

    private Dictionary<EnemyType, List<GameObject>> enemyPool = new Dictionary<EnemyType, List<GameObject>>();


    private int initObjectCount = 50; // 처음에 Pool 에 넣어줄 오브젝트 수

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameObject obj = GameObject.Find("Pos");

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            int a = i;
            enemyDestList.Add(obj.transform.GetChild(a));
        }
        InitPool();
    }

    /// <summary>
    /// Pool 초기화
    /// </summary>
    private void InitPool()
    {
        for (int type = 0; type < (int)EnemyType.END_OF_ENUM; ++type)
        {
            enemyPool.Add((EnemyType)type, new List<GameObject>());

            for (int j = 0; j < initObjectCount; ++j)
            {
                enemyPool[(EnemyType)type].Add(Create((EnemyType)type));
            }
        }
    }

    /// <summary>
    /// Pool 에 적을 추가합니다.
    /// </summary>
    /// <param name="type">타입</param>
    private GameObject Create(EnemyType type)
    {
        GameObject temp = Instantiate(enemyPrefabs[(int)type], enemyDestList[0].position, Quaternion.identity, transform);
        temp.GetComponent<AIMove>().SetDest(enemyDestList);
        temp.SetActive(false);
        return temp;
    }

    /// <summary>
    /// 적을 하나 가져옵니다.
    /// </summary>
    /// <param name="type">가저올 적 타입</param>
    /// <returns>GameObject of enemy</returns>
    public GameObject Get(EnemyType type)
    {
        GameObject temp = enemyPool[type].Find(x => !x.activeSelf);

        if(temp == null)
        {
            temp = Create(type);
        }

        temp.SetActive(true);
        return temp;
    }

    // TODO : 적 스폰 기능, Pool, type[], delay 만큼 넣어줘야 함

    /// <summary>
    /// 적을 스폰합니다.
    /// </summary>
    /// <param name="type">스폰할 적의 타입</param>
    public void Spawn(EnemyType type)
    {
        ActiveEnemyManager.Instance.Spawn(Get(type));
    }

}
