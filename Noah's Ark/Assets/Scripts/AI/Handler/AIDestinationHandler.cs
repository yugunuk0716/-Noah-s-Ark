using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDestinationHandler : MonoBehaviour
{
    AIMove move;

    private void Start()
    {
        move = GetComponent<AIMove>();
        // 비활성화
        move.OnFinalDestinationArrived += () => ActiveEnemyManager.Instance.Remove(move.gameObject);

    }
}
