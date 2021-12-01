using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public TowerGroundState state;

    private void Awake()
    {
        state = TowerGroundState.None;
    }


}
