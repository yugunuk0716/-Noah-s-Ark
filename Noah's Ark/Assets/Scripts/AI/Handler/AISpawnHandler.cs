using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnHandler : MonoBehaviour
{
    AIHealth health;
    AIMove move;
    AIAnimation anim;


    private void Start()
    {
        health = GetComponent<AIHealth>();
        move   = GetComponent<AIMove>();
        anim   = GetComponent<AIAnimation>();

        ActiveEnemyManager.Instance.OnSpawnEnemy += (enemy) => {
            health.HP = health.DefaultHP;
            move.SetDefaultSpeed((int)anim.CurrentRunMode); // RunMode 에 따른 속도
            // anim.SetRunmode();
        };
    }

    

}
