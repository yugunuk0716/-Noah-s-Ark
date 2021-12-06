using UnityEngine;

public class AIBase : MonoBehaviour
{
    /// <summary>
    /// 스폰 시 호출됨.
    /// </summary>
    public event System.Action OnSpawn;

    private void Awake()
    {
        OnSpawn += () => { };
    }

    private void OnEnable()
    {
        OnSpawn();
    }
}