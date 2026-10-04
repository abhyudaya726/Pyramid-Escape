using UnityEngine;

public class GameComplete : MonoBehaviour
{
    private bool completed;

    private void OnTriggerEnter(Collider other)
    {
        if (completed)
            return;

        if (!other.CompareTag("Player"))
            return;

        completed = true;

        Debug.Log("GAME COMPLETED!");

        QuitGame();
    }

    private void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}