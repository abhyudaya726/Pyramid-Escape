using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameComplete : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI gameCompleteText;

    private bool completed;

    private void Start()
    {
        if (gameCompleteText != null)
        {
            gameCompleteText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (completed)
            return;

        if (!other.CompareTag("Player"))
            return;

        completed = true;

        Debug.Log("GAME COMPLETED!");

        if (gameCompleteText != null)
        {
            gameCompleteText.gameObject.SetActive(true);
        }

        StartCoroutine(LoadMainMenuAfterDelay());
    }

    private IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }
}