using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FORGE3D;


public class GameManager : MonoSingleton<GameManager>
{
    private int _money = 1;
    public int Money 
    {
        get {
            return _money;
        }

        set {
            _money = value;
            UIManganager.Instance.moneyText.text = string.Format("설치 가능한 타워 수 :  {0} 개", _money);
        }
    }

    private int _hp = 100;
    public int maxHp = 100;
    public int Hp
    {
        get {
            return _hp;
        }

        set {
            _hp = value;
            UIManganager.Instance.hpBar.fillAmount = (float)_hp /maxHp;
        }
    }


    private int _mp = 100;
    private int maxMp = 100;
    public int Mp
    {
        get
        {
            return _mp;
        }

        set
        {
            _mp = value;
            UIManganager.Instance.mpBar.fillAmount = (float)_mp / maxMp;
        }
    }
    private void Awake()
    {
        //StageManager.instance.InitStage();
    }


    private void Start()
    {
        Money = 1;
        Hp = 100;
        Mp = 100;

        UIManganager.Instance.hpBar.fillAmount = (float)_hp / maxHp;
        UIManganager.Instance.mpBar.fillAmount = (float)_mp / maxMp;
        UIManganager.Instance.moneyText.text = string.Format("설치 가능한 타워 수 :  {0} 개", _money);

        
    }



}

