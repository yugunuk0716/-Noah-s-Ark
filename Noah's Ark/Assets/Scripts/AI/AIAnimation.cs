using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AIAnimation : MonoBehaviour
{

    public enum RunMode
    {
        IDLE = 0,
        WALK,
        RUN,
        DASH
    }

    public RunMode CurrentRunMode { get; private set; } = RunMode.IDLE;

    private Animator animator;

    private int runModeHash = Animator.StringToHash("RunMode");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 적 에니메이션의 Runmode 를 설정합니다.
    /// </summary>
    /// <param name="mode"></param>
    public void SetRunmode(RunMode mode)
    {
        animator.SetInteger(runModeHash, (int)mode);
        CurrentRunMode = mode;
    }
}
