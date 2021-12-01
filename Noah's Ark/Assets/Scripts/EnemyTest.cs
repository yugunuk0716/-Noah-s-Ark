using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) 
        {
            print("Hp--");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Bullet"))
        {
            print("Hp--");
        }
    }
}
