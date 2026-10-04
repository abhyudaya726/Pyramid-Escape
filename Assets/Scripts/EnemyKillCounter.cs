using UnityEngine;

public class EnemyKillCounter : MonoBehaviour
{
    [Header("Required Kills")]
    [SerializeField] private int requiredKills = 2;

    [Header("Locked Door")]
    [SerializeField] private GameObject lockedDoor;

    private int currentKills;
    private bool doorUnlocked;

    public void RegisterKill()
    {
        if (doorUnlocked)
            return;

        currentKills++;

        Debug.Log(
            "Enemies killed: " +
            currentKills +
            "/" +
            requiredKills
        );

        if (currentKills >= requiredKills)
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        doorUnlocked = true;

        Debug.Log("2 enemies killed! Door unlocked!");

        if (lockedDoor != null)
        {
            lockedDoor.SetActive(false);
        }
    }
}