using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingTowerBehaviour : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowStart;
    public float fireRate = 1.0f;
    public float range = 10.0f;
    public float lifetime = 3.0f;

    bool canShoot = true;

    private MeshRenderer showRangeMeshRenderer;
    private Transform targetEnemy;
    private AudioSource rapidFireSFX;
    private Enemy enemyType;

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
    // Start is called before the first frame update
    void Start()
    {

        Transform meshRendTransform = transform.Find("showRange");

        rapidFireSFX = GetComponent<AudioSource>();

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
    // Update is called once per frame
    void Update()
    {
/*        if (targetEnemy != null)
        {
            Debug.Log("Distance to target: " + Vector3.Distance(transform.position, targetEnemy.position));
            Debug.Log("Range: " + range);
        }*/
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyType = other.GetComponent<Enemy>();

            if (enemyType != null)
            {
                if (enemyType.enemyType == Enemy.EnemyType.AIR)
                {
                    Debug.Log("Enemy IN range!");
                    shootProjectile(other.transform);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("test test test");
        }
    }
    private IEnumerator shootProjectile(Transform enemy)
    {
        if (arrowPrefab != null && arrowStart != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowStart.position, Quaternion.identity);
            arrow.transform.LookAt(enemy.position);
            Vector3 direction = (enemy.position - arrowStart.position).normalized;
            arrow.GetComponent<Rigidbody>().velocity = direction * range;
            Destroy(arrow, lifetime);

            if (rapidFireSFX != null)
            {
                rapidFireSFX.Play();  // Play the shooting sound effect
            }
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    private void FindNewTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemyType = collider.gameObject.GetComponent<Enemy>();

                if (enemyType != null)
                {
                    if (enemyType.enemyType == Enemy.EnemyType.AIR)
                    {
                        targetEnemy = collider.transform;
                        break;
                    }
                }
            }
        }
    }
    private bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, targetEnemy.position) <= range;
    }
}
