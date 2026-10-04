using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 8f;

    [Header("Combat")]
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private int attackDamage = 25;
    [SerializeField] private float attackCooldown = 1.2f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private PlayerHealth playerHealth;
    private float nextAttackTime;

    private void Awake()
    {
        // Find player
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        // Find PlayerHealth
        if (player != null)
        {
            playerHealth =
                player.GetComponent<PlayerHealth>();
        }

        // Find Animator
        if (animator == null)
        {
            animator =
                GetComponentInChildren<Animator>();
        }

        // Debug checks
        if (player == null)
        {
            Debug.LogError("EnemyAI: Player not found!");
        }

        if (playerHealth == null)
        {
            Debug.LogError(
                "EnemyAI: PlayerHealth not found on Player!"
            );
        }

        if (animator == null)
        {
            Debug.LogError(
                "EnemyAI: Animator not found!"
            );
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        if (distance > detectionRange)
        {
            StopMoving();
            return;
        }

        if (distance <= attackRange)
        {
            StopMoving();
            FacePlayer();

            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }

        FacePlayer();

        transform.position +=
            transform.forward *
            moveSpeed *
            Time.deltaTime;
    }

    private void StopMoving()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void FacePlayer()
    {
        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
    }

    private void AttackPlayer()
    {
        if (playerHealth == null)
        {
            Debug.LogError(
                "EnemyAI: Cannot attack because PlayerHealth is missing!"
            );

            return;
        }

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        playerHealth.TakeDamage(attackDamage);

        Debug.Log(
            "ENEMY ATTACKED! Damage: " +
            attackDamage
        );

        nextAttackTime =
            Time.time + attackCooldown;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );

        Gizmos.DrawWireSphere(
            transform.position,
            attackRange
        );
    }
}