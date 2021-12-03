using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    /// <summary>
    /// 목적지에 도착하면 호출됩니다.
    /// </summary>
    public Action OnDestinationArrived;

    /// <summary>
    /// 마지막 목적지에 도착하면 호출됩니다.
    /// </summary>
    public Action OnFinalDestinationArrived;

    // AI 목적지
    public List<Transform> defaultDestination = new List<Transform>(); // 기본 목적지
    private List<Transform> destination = new List<Transform>(); // 실제 목적지

    private NavMeshAgent agent; // agent
    private float defaultSpeed = 0.0f; // 기본 속도
    private int currentDestIdx = 0; // 목적지 인덱스

    private Coroutine currentSpeedEffect = null; // 속도 원상복귀 저장용

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
        ++currentDestIdx;
        if(currentDestIdx >= destination.Count)
        {
            OnFinalDestinationArrived();
            return;
        }

        agent.destination = destination[currentDestIdx].position;
        OnDestinationArrived();
    }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        for (int i = 0; i < defaultDestination.Count; ++i) // 복사
        {
            destination.Add(defaultDestination[i]);
        }

        OnFinalDestinationArrived += () => { };
        OnDestinationArrived      += () => { };
    }

    protected virtual void Start()
    {
        agent.destination = destination[0].position;
        defaultSpeed = agent.speed;
    }

    protected virtual void Update()
    {
        if(agent.remainingDistance <= 0.1f)
        {
            ToNextDestination();
        }
    }

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
        agent.speed = defaultSpeed;
        
    }
}
