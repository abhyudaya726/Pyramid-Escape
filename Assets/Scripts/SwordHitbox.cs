using UnityEngine;
using System.Collections.Generic;

public class SwordHitbox : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 25;

    [Header("Hit Detection")]
    [SerializeField] private float hitRadius = 0.7f;
    [SerializeField] private LayerMask enemyLayer;

    private bool isActive;

    private HashSet<EnemyHealth> hitEnemies =
        new HashSet<EnemyHealth>();

    public void StartHit()
    {
        // Already active, so don't reset the hit list.
        if (isActive)
            return;

        isActive = true;
        hitEnemies.Clear();
    }

    public void EndHit()
    {
        isActive = false;
    }

    private void Update()
    {
        if (!isActive)
            return;

        CheckForEnemies();
    }

    private void CheckForEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            hitRadius,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy =
                hit.GetComponentInParent<EnemyHealth>();

            if (enemy == null)
                continue;

            if (hitEnemies.Contains(enemy))
                continue;

            hitEnemies.Add(enemy);

            enemy.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            hitRadius
        );
    }
}