using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 1;
    public float lifeTime = 1;
    public LayerMask enemyLayer;
    [Range(0, 5)]
    public float explosionRange = 1;
    [Range(0, 5)]
    public int explosionDamage = 1;
    public GameObject explosionFeedback;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Instantiate(explosionFeedback, transform.position, transform.rotation);
            enemy.GetComponentInParent<HealthComponent>().TakeDamage(explosionDamage);
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
