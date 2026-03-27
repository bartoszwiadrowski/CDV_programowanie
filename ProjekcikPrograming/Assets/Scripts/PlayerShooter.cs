using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public string targetTag = "Enemy";
    public float fireRate = 1f;
    public float projectileSpeed = 15f;
    public float range = 20f;

    private Collider[] playerColliders;

    void Start()
    {
        playerColliders = GetComponentsInChildren<Collider>();
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Transform closestTarget = GetClosestTarget();
        
        if (closestTarget == null) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Collider projCollider = proj.GetComponent<Collider>();
        
        if (projCollider != null && playerColliders != null)
        {
            foreach (Collider pc in playerColliders)
            {
                Physics.IgnoreCollision(pc, projCollider);
            }
        }

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            rb = proj.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }

        rb.isKinematic = false;
        
        Vector3 direction = (closestTarget.position - firePoint.position).normalized;
        proj.transform.forward = direction;
        rb.linearVelocity = direction * projectileSpeed;
    }

    private Transform GetClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject t in targets)
        {
            float distance = Vector3.Distance(t.transform.position, currentPosition);
            if (distance < minDistance && distance <= range)
            {
                closest = t.transform;
                minDistance = distance;
            }
        }

        return closest;
    }
}