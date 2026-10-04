using UnityEngine;
using System.Collections.Generic;

public class SwordHitbox : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 25;

    private bool isActive;

    private HashSet<EnemyHealth> hitEnemies =
        new HashSet<EnemyHealth>();

    private Collider hitboxCollider;

    private void Awake()
    {
        hitboxCollider = GetComponent<Collider>();

        if (hitboxCollider == null)
        {
            Debug.LogError("SwordHitbox: No Collider found!");
            return;
        }

        hitboxCollider.isTrigger = true;
        hitboxCollider.enabled = false;
    }

    public void StartHit()
    {
        isActive = true;
        hitEnemies.Clear();

        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = true;
        }
    }

    public void EndHit()
    {
        isActive = false;

        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;

        EnemyHealth enemy =
            other.GetComponentInParent<EnemyHealth>();

        if (enemy == null)
            return;

        if (hitEnemies.Contains(enemy))
            return;

        hitEnemies.Add(enemy);

        enemy.TakeDamage(damage);
    }
}