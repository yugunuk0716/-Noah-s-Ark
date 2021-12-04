using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    //구조짜기 귀찮아서 이렇게 함 -> 나중에 이렇게 해결이 안될시 바꿈
    public Dictionary<string, Queue<Action>> itemDic;

    private void Awake()
    {
        itemDic = new Dictionary<string, Queue<Action>>();

        //테스트용코드
        ItemAdd("EnemySlow", EnemySlowItem);
    }

    private void Update()
    {
        //테스트용 코드
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem("EnemySlow");
        }
    }
    /// <summary>
    ///  아이템 추가하는 함수
    /// </summary>
    /// <param name="name">아이템 이름</param>
    /// <param name="act">아이템 효과</param>
    public void ItemAdd(string name, Action act)
    {
        if(itemDic.ContainsKey(name))
        {
            itemDic[name].Enqueue(act);
            return;
        }
        itemDic.Add(name, new Queue<Action>());
        itemDic[name].Enqueue(act);
    }
    /// <summary>
    /// 아이템을 사용하는 함수
    /// </summary>
    /// <param name="name">아이템 이름</param>
    public void UseItem(string name)
    {
        Debug.Log($"{name} : use");
        if(itemDic.ContainsKey(name) && itemDic[name].Count > 0)
        {
            itemDic[name].Dequeue().Invoke();
        }
        else
        {
            Debug.LogError("이 아이템이 없습니다");
        }
    }

    public void EnemySlowItem()
    {
        for (int i = 0; i < EnemyPoolManager.Instance.enemyList.Count; i++)
        {
            //임시로 3초간 슬로우
            EnemyPoolManager.Instance.enemyList[i].SetSpeed(0.5f, 3f);
        }
    }

    
}
