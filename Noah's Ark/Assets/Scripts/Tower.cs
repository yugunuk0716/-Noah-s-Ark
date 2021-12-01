using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerState { SearchTarget = 0, AttackToTarget,}

public class Tower : MonoBehaviour
{
    //public GameObject bulletPrefab;
    //public Transform fireTrm;
    [SerializeField]
    private float attackRate = 0.5f;
    [SerializeField]
    private float attackRange = 2.0f;

    private TowerState towerState = TowerState.SearchTarget;
    [SerializeField]
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
}
