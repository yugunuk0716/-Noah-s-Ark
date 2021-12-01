using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerState { SearchTarget = 0, AttackToTarget, }
public enum TowerGroundState { None = 0, Builded, }

public class Tower : MonoBehaviour
{
    //public GameObject bulletPrefab;
    //public Transform fireTrm;
    [SerializeField]
    private float attackRate = 0.5f;
    [SerializeField]
    private float attackRange = 2.0f;

    private TowerState towerState = TowerState.SearchTarget;
    //[SerializeField]
    private Transform attackTarget = null;

    public void ChangeState(TowerState newState)
    {
        StopCoroutine(towerState.ToString());
        towerState = newState;
        StartCoroutine(towerState.ToString());
    }

    private void Update()
    {
        if(attackTarget != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        Vector3 vec = attackTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation(vec.normalized);
    }

    IEnumerator SearchTarget()
    {
        while (true)
        {
            
            if(attackTarget != null)
            {
                ChangeState(TowerState.AttackToTarget);
            }

            yield return null;
        }
    }

    IEnumerator AttackToTarget()
    {
        while (true)
        {
            if(attackTarget == null)
            {
                ChangeState(TowerState.SearchTarget);
                break;
            }
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(TowerState.SearchTarget);
                break;
            }

            yield return new WaitForSeconds(attackRate);

            //АјАн
        }
    }
}
