using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 100.0f;
    public float arrowDmg = 1.0f;
    public float cannonDmg = 50.0f;
    //public float speedIncrement = 0.1f;

    private NavMeshAgent navMesh;

    private void Start()
    {
       // navMesh = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("HIT HIT HIT");
            //navMesh.speed += speedIncrement;
            health = health - arrowDmg;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Cannon"))
        {
            Debug.Log("HIT HIT HIT");
            //navMesh.speed += speedIncrement;
            health = health - cannonDmg;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
