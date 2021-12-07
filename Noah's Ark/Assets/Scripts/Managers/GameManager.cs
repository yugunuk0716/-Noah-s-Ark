using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FORGE3D;


public class GameManager : MonoSingleton<GameManager>
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }


    private void Awake()
    {
        _instance = this;
    }

    public Transform enemySpawnPosition = null;

    private int money = 0;


  

}

