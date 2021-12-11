using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerGroundState
{
    None = 0,
    Builded,
}

public class Ground : MonoBehaviour
{
    public TowerGroundState state;
    
    public Transform towerPos;
    public SpriteRenderer attackRange;

    private void Awake()
    {
        state = TowerGroundState.None;
        towerPos = transform.GetChild(0);
        attackRange = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void ChangeTowerGroundState(TowerGroundState state)
    {
        this.state = state;
    }
}
