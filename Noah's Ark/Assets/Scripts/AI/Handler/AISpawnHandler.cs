using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnHandler : MonoBehaviour
{
    AIHealth health;
    AIMove move;
    AIBase aiBase;
    AIAnimation anim;


    private void Awake()
    {
        health = GetComponent<AIHealth>();
        move   = GetComponent<AIMove>();
        anim   = GetComponent<AIAnimation>();
        aiBase = GetComponent<AIBase>();

        ActiveEnemyManager.Instance.OnSpawnEnemy += OnSpawn; // TODO : 이거 조금 잘못됨. 하나 생성될때 전부 호출됨
    }

    private void OnSpawn(GameObject enemy)
    {
        Debug.Log("OnSpawnEnemy");
        health.HP = health.DefaultHP;
        move.SetDefaultSpeed((int)anim.CurrentRunMode); // RunMode 에 른 속도
        anim.SetRunmode((AIAnimation.RunMode)aiBase.GetEnemyType());
        ActiveEnemyManager.Instance.OnSpawnEnemy -= OnSpawn;
    }

    

}
