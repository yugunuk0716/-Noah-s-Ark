using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnHandler : MonoBehaviour, ISpawnable
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
    }

    public void OnSpawn(GameObject enemy)
    {
        Debug.Log("OnSpawnEnemy");
        health.HP = health.DefaultHP + (int)aiBase.GetEnemyType() * 5;
        move.SetDefaultSpeed((int)anim.CurrentRunMode); // RunMode 에 른 속도
        Debug.Log("Type:" + aiBase.GetEnemyType());
        anim.SetRunmode((AIAnimation.RunMode)aiBase.GetEnemyType());
        ActiveEnemyManager.Instance.OnSpawnEnemy -= OnSpawn;
    }

    

}
