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
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            int lastHP = _hp;
            _hp = value;

            if (_hp < lastHP) // 공격 받은 경우만
            {
                if (_hp > 0) OnHealthDecreased(); // 데미지
                else OnDead(); // 사망
            }

        }
    }
    public int DefaultHP { get; private set; }

    protected virtual void Awake()
    {
        OnHealthDecreased += () => { };
        OnDead += () => { };

        DefaultHP = HP;
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("BULLET"))
        {
            HP -= 1;
        }

        if(other.CompareTag("SKILL"))
        {
            HP -= 5;
        }
    }

    
}
