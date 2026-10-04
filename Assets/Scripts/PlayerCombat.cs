using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SwordHitbox swordHitbox;

    [Header("Combat Settings")]
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;

    [Header("Sword Hit Timing")]
    [SerializeField] private float hitStartTime = 0.4f;
    [SerializeField] private float hitEndTime = 0.6f;

    private bool isAttacking;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        HandleAttack();
        HandleSwordHitbox();
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(attackKey) && !isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    private void HandleSwordHitbox()
    {
        if (!isAttacking)
            return;

        AnimatorStateInfo stateInfo =
            animator.GetCurrentAnimatorStateInfo(0);

        // Check for the lowercase attack state
        if (!stateInfo.IsName("attack"))
        {
            swordHitbox.EndHit();
            isAttacking = false;
            return;
        }

        float normalizedTime = stateInfo.normalizedTime;

        // Frames 12-18 of a 30-frame animation = 40%-60%
        if (normalizedTime >= hitStartTime &&
            normalizedTime < hitEndTime)
        {
            swordHitbox.StartHit();
        }
        else
        {
            swordHitbox.EndHit();
        }

        if (normalizedTime >= 1f)
        {
            swordHitbox.EndHit();
            isAttacking = false;
        }
    }
}