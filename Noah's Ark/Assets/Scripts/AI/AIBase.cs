using UnityEngine;

public class AIBase : MonoBehaviour
{
    [SerializeField] private EnemyType type;

    public EnemyType GetEnemyType()
    {
        return type;
    }

}