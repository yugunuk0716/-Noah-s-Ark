using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    //����¥�� �����Ƽ� �̷��� �� -> ���߿� �̷��� �ذ��� �ȵɽ� �ٲ�
    public Dictionary<string, Queue<Action>> itemDic;

    private void Awake()
    {
        itemDic = new Dictionary<string, Queue<Action>>();

        //�׽�Ʈ���ڵ�
        ItemAdd("EnemySlow", EnemySlowItem);
    }

    private void Update()
    {
        //�׽�Ʈ�� �ڵ�
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem("EnemySlow");
        }
    }
    /// <summary>
    ///  ������ �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="name">������ �̸�</param>
    /// <param name="act">������ ȿ��</param>
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
    /// �������� ����ϴ� �Լ�
    /// </summary>
    /// <param name="name">������ �̸�</param>
    public void UseItem(string name)
    {
        Debug.Log($"{name} : use");
        if(itemDic.ContainsKey(name) && itemDic[name].Count > 0)
        {
            itemDic[name].Dequeue().Invoke();
        }
        else
        {
            Debug.LogError("�� �������� �����ϴ�");
        }
    }

    public void EnemySlowItem()
    {
        for (int i = 0; i < EnemyPoolManager.Instance.enemyList.Count; i++)
        {
            //�ӽ÷� 3�ʰ� ���ο�
            EnemyPoolManager.Instance.enemyList[i].SetSpeed(0.5f, 3f);
        }
    }

    
}
