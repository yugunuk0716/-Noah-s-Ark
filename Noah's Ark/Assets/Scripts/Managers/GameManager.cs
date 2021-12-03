using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FORGE3D;


public class GameManager : MonoSingleton<GameManager>
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("�������� GameManager�� �������Դϴ�");
        }
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}

