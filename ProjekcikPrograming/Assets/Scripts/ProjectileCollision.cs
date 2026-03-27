using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ProjectileCollision : MonoBehaviour
{
    public int damage = 1;
    public float destroyDelay = 0.05f;
    public float maxLifetime = 5f;

    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
            Destroy(gameObject, destroyDelay);
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}