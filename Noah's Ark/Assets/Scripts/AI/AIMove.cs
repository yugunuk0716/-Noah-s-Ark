using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour, IMoveManagement
{
    /// <summary>
    /// 목적지에 도착하면 호출됩니다.
    /// </summary>
    public Action OnDestinationArrived;

    /// <summary>
    /// 마지막 목적지에 도착하면 호출됩니다.
    /// </summary>
    public Action OnFinalDestinationArrived;

    // 기본 스폰 포지션
    public Transform defaultSpawnPosition;

    private List<Transform> destination = new List<Transform>(); // 실제 목적지

    private NavMeshAgent agent; // agent

    private float _defaultSpeed = 0.0f;
    public float DefaultSpeed { get; } // 기본 속도

    private int _currentDestIdx = 0;
    public int CurrentDestIdx { get; } // 목적지 인덱스

    private Coroutine currentSpeedEffect = null; // 속도 원상복귀 저장용

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        OnFinalDestinationArrived += () => { };
        OnDestinationArrived += () => { };
    }

    protected virtual void Start()
    {
        agent.destination = destination[0].position;
        _defaultSpeed = agent.speed;
    }

    protected virtual void Update()
    {
        if (agent.remainingDistance <= 0.1f)
        {
            ToNextDestination();
        }
    }

    /// <summary>
    /// 적의 목적지, 포지션을 리셋합니다.
    /// </summary>
    public void ResetAI()
    {
        agent.destination  = destination[0].position;
        transform.position = destination[0].position;
        _currentDestIdx    = 0;
    }


#region 목적지

    /// <summary>
    /// AI가 지나갈 목적지를 추가합니다.
    /// </summary>
    /// <param name="dest">추가할 목적지</param>
    public void AddDestination(Transform dest)
    {
        destination.Add(dest);
    }

    /// <summary>
    /// 다음 목적지로 전환합니다.
    /// </summary>
    protected virtual void ToNextDestination()
    {
        ++_currentDestIdx;
        if(_currentDestIdx >= destination.Count)
        {
            agent.ResetPath();
            OnFinalDestinationArrived();
            return;
        }

        agent.destination = destination[_currentDestIdx].position;
        OnDestinationArrived();
    }

#endregion

#region 속도

    /// <summary>
    /// AI 속도를 설정합니다.<br/>
    /// 이미 감소, 상승이 진행중이면 덮어씁니다.
    /// </summary>
    /// <param name="multipy">비율</param>
    /// <param name="duration">기간 (second)<br/>0: 종료, -1: 영구</param>
    public void SetSpeed(float multipy, float duration)
    {
        agent.speed *= multipy;

        if(currentSpeedEffect != null)
            StopCoroutine(currentSpeedEffect);

        if(duration != -1.0f)
            currentSpeedEffect = StartCoroutine(SetSpeedToNormal(duration));
    }

    private IEnumerator SetSpeedToNormal(float duration) // 속도 원상복귀 용도
    {
        yield return new WaitForSeconds(duration);
        agent.speed = _defaultSpeed;
    }

    /// <summary>
    /// 기본 속도를 설정합니다.<br/>
    /// Spawn 이외에서 호출되면 안됩니다.
    /// </summary>
    public void SetDefaultSpeed(float speed)
    {
        _defaultSpeed = speed;
    }
#endregion
    
#region 거리
    public float GetRemainDistance()
    {
        return agent.remainingDistance;
    }

    public void SetDest(List<Transform> dest)
    {
        for (int i = 0; i < dest.Count; ++i) // 복사
        {
            destination.Add(dest[i]);
        }
    }
#endregion
}
