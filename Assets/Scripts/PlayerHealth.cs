using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    [Header("Health Bar")]
    [SerializeField] private RectTransform healthBarFill;
    [SerializeField] private float healthBarMaxWidth = 200f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private int currentHealth;
    private bool isDead;

    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

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

        Debug.Log("Player Health: " + currentHealth);

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

        Debug.Log("PLAYER DIED!");

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (playerCombat != null)
        {
            playerCombat.enabled = false;
        }

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        StartCoroutine(ReloadSceneAfterDelay());
    }

    private IEnumerator ReloadSceneAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }
}