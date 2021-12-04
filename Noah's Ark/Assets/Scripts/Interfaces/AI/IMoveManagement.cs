using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IMoveManagement
{
    public int CurrentDestIdx { get; }
    public float DefaultSpeed { get; }

    public float GetRemainDistance();
    public void SetSpeed(float multipy, float duration);
}
