using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthEventHandler : MonoBehaviour
{
    [SerializeField] private int moneyGain = 2;
    [SerializeField] private int mpGain = 5;

    AIHealth health;

    private void Start()
    {
        health = GetComponent<AIHealth>();
        // 사망
        health.OnDead += () => {
            ActiveEnemyManager.Instance.Remove(health.gameObject);
            GameManager.Instance.Money += moneyGain;
            GameManager.Instance.Mp += mpGain;
        };
    }
}
