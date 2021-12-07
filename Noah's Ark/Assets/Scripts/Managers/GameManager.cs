using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FORGE3D;


public class GameManager : MonoSingleton<GameManager>
{
    private int _money = 0;
    public int Money 
    {
        get {
            return _money;
        }

        set {
            _money = value;
        }
    }

    private int _hp = 100;
    public int Hp
    {
        get {
            return _hp;
        }

        set {
            _hp = value;
        }
    }



}

