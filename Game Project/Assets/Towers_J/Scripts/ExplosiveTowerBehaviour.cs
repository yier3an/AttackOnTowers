using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ExplosiveTowerBehaviour : MonoBehaviour
{
    public GameObject cannonPrefab;
    public Transform cannonStart;
    public GameObject particleSysPrefab;

    public float fireRate = 1.0f;
    public float speed = 10.0f;
    public float lifetime = 3.0f;

    private float explosionRadius = 20.0f; // Radius of the explosion
    private int explosionDamage = 50; // Damage caused by the explosion

    bool canShoot = true;

    private MeshRenderer showRangeMeshRenderer;
    private Transform targetEnemy;
    private AudioSource cannonBallSFX;
    private Enemy enemyType;

    private IEnumerator shootProjectile(Transform enemy)
    {
        if (enemy != null)
        {
            if (cannonPrefab != null && cannonStart != null)
            {
                GameObject cannon = Instantiate(cannonPrefab, cannonStart.position, Quaternion.identity);
                cannon.transform.LookAt(enemy.position);
                Vector3 direction = (enemy.position - cannonStart.position).normalized;

                Rigidbody cannonBall = cannon.GetComponent<Rigidbody>();
                cannonBall.velocity = direction * speed;

                // Add a script to handle the explosion when the cannonball collides
                CannonBall explosionScript = cannon.AddComponent<CannonBall>();
                explosionScript.explosionRadius = explosionRadius;
                explosionScript.explosionDamage = explosionDamage;
                explosionScript.particleSysPrefab = particleSysPrefab;

               Destroy(cannon, lifetime);

                if (cannonBall != null)
                {
                    cannonBallSFX.Play();
                }
            }
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    private void ToggleShowRange(bool show)
    {
        if (showRangeMeshRenderer != null)
        {
            showRangeMeshRenderer.enabled = show;
        }
    }
    private void OnMouseEnter()
    {
        ToggleShowRange(true);
    }
    private void OnMouseExit()
    {
        ToggleShowRange(false);
    }
    private bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, targetEnemy.position) <= explosionRadius;
    }
    private void FindNewTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemyType = collider.gameObject.GetComponent<Enemy>();

                if (enemyType != null)
                {
                    if (enemyType.enemyType == Enemy.EnemyType.GROUND)
                    {
                        targetEnemy = collider.transform;
                        break;
                    }
                }
            }
        }
    }
   private void Start()
    {
        Transform meshRendTransform = transform.Find("showRange");

        cannonBallSFX = GetComponent<AudioSource>();

        if (meshRendTransform != null)
        {
            showRangeMeshRenderer = meshRendTransform.GetComponent<MeshRenderer>();

            if (showRangeMeshRenderer != null)
            {
                showRangeMeshRenderer.enabled = false;
            }
        }
        else
        {
            Debug.LogError("showRange is not Found");
        }
    }
    private void Update()
    {
        if (canShoot)
        {
            if (targetEnemy == null || !IsTargetInRange())
            {
                FindNewTarget();
            }
            if (targetEnemy != null && IsTargetInRange())
            {
                StartCoroutine(shootProjectile(targetEnemy));
                canShoot = false;
            }
        }
    }
}
