using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowTowerBehaviour : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowStart;

    public float fireRate = 1.0f;
    public float range = 10.0f;
    public float lifetime = 3.0f;

    private float fireTimer;
    private Rigidbody rb;

    bool canShoot = true;

    private MeshRenderer showRangeMeshRenderer;
    private Transform targetEnemy;
    private AudioSource shootArrowSFX;

    private void OnMouseEnter()
    {
        ToggleShowRange(true);
    }
    private void OnMouseExit()
    {
        ToggleShowRange(false);
    }
    private void ToggleShowRange(bool show)
    {
        if (showRangeMeshRenderer != null)
        {
            showRangeMeshRenderer.enabled = show;
        }
    }
    private bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, targetEnemy.position) <= range;
    }
    private void FindNewTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                targetEnemy = collider.transform;
                break;
            }
        }
    }
    private IEnumerator shootProjectile(Transform enemy)
    {
        if (enemy != null)
        {
            if (arrowPrefab != null && arrowStart != null)
            {
                GameObject arrow = Instantiate(arrowPrefab, arrowStart.position, Quaternion.identity);
                arrow.transform.LookAt(enemy.position);
                Vector3 direction = (enemy.position - arrowStart.position).normalized;
                arrow.GetComponent<Rigidbody>().velocity = direction * range;
                Destroy(arrow, lifetime);

                if (shootArrowSFX != null)
                {
                    shootArrowSFX.Play();  // Play the shooting sound effect
                }
            }
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    private void Start()
    {
        Transform meshRendTransform = transform.Find("showRange");
        
        shootArrowSFX = GetComponent<AudioSource>();

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
