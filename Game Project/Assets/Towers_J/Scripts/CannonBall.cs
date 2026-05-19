using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float explosionRadius = 5.0f;
    public int explosionDamage = 50;
    public GameObject particleSysPrefab;
    private Enemy enemyType;

    private void OnCollisionEnter(Collision collision)
    {
         // Trigger the explosion
            Explode();
    }
    private void Explode()
    {
        // Instantiate particle effects at the explosion point
        if (particleSysPrefab != null)
        {
            Instantiate(particleSysPrefab, transform.position, Quaternion.identity);
        }
        // Find all enemies within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (hitCollider.CompareTag("Enemy"))
            {
                enemyType = hitCollider.gameObject.GetComponent<Enemy>();
                if (enemyType != null)
                {
                    if (enemyType.enemyType == Enemy.EnemyType.GROUND)
                    {
                        // from steve
                        Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.AddExplosionForce(10.0f, transform.position, 10.0f, 3.0f, ForceMode.Impulse);
                        }
                        enemy.Damage(explosionDamage);
                    }
                }
            }
        }
        // Destroy the cannonball after the explosion
        Destroy(gameObject);
    }
}