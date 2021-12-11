using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject[] mapPrfabs;

    private int _stage;
    public int Stage
    {
        get
        {
            return _stage;
        }
        set
        {
            _stage = value;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject InitStage()
    {
        return Instantiate(mapPrfabs[Stage]);
    }
}
