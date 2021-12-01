using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    /// <summary>
    /// 채력 감소 시 호출<br/>사망 시 호출 안됨
    /// </summary>
    public event Action OnHealthDecreased;

    /// <summary>
    /// 사망 시 호출
    /// </summary>
    public event Action OnDead;

    [SerializeField] private int _hp = 20;
    public int HP { 
        get {
            return _hp;
        }
        set {
            _hp = value;
            switch(_hp <= 0)
            {
                case true: // 사망
                    OnDead();
                    break;
                case false: // 채력 감소
                    OnHealthDecreased();
                    break;
            }
        } 
    }


    protected virtual void Awake()
    {
        OnHealthDecreased += () => { };
        OnDead += Dead;
    }

    /// <summary>
    /// 사망 시 호출됨
    /// </summary>
    protected virtual void Dead()
    {
        gameObject.SetActive(false);
    }
}
