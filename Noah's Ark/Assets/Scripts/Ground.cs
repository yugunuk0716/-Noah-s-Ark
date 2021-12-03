using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public TowerGroundState state;

    public Transform towerPos;

    private void Awake()
    {
        state = TowerGroundState.None;
    }

    public void ChangeTowerGroundState(TowerGroundState state)
    {
        this.state = state;
    }
}
