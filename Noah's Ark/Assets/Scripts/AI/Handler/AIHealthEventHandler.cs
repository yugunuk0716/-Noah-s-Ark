using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthEventHandler : MonoBehaviour
{
    [SerializeField] private int mpGain = 5;

    AIHealth health;

    private void Start()
    {
        health = GetComponent<AIHealth>();
        // 사망
        health.OnDead += () => {
            ActiveEnemyManager.Instance.Remove(health.gameObject);
            GameManager.Instance.Mp += mpGain;
        };
    }


}
