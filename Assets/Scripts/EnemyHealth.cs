using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    [Header("Health Bar")]
    [SerializeField] private RectTransform healthBarFill;
    [SerializeField] private float healthBarMaxWidth = 150f;

    private int currentHealth;
    private bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill == null)
            return;

        float healthPercent =
            (float)currentHealth / maxHealth;

        healthBarFill.sizeDelta =
            new Vector2(
                healthBarMaxWidth * healthPercent,
                healthBarFill.sizeDelta.y
            );
    }

    private void Die()
    {
        isDead = true;

        EnemyKillCounter counter =
            FindFirstObjectByType<EnemyKillCounter>();

        if (counter != null)
        {
            counter.RegisterKill();
        }

        gameObject.SetActive(false);
    }
}