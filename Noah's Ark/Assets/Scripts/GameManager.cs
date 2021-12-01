using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FORGE3D;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    private List<F3DTurret> turrets = new List<F3DTurret>();

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("여러개의 GameManager가 실행중입니다");
        }
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetTurret(F3DTurret turret) 
    {
        turrets.Add(turret);
    }

    public List<F3DTurret> GetTurret() 
    {
        return turrets;
    }
}

