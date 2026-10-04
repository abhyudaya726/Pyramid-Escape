using UnityEngine;

public class SwordPickup : MonoBehaviour, IInteractable
{
    [Header("Sword Setup")]
    [SerializeField] private Transform swordHand;

    private bool isPickedUp;

    public void Interact()
    {
        if (isPickedUp)
            return;

        PickUpSword();
    }

    private void PickUpSword()
    {
        isPickedUp = true;

        // Stop the sword from interacting with the world
        Collider swordCollider = GetComponent<Collider>();

        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }

        // Attach sword to player's hand
        transform.SetParent(swordHand);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log("Sword picked up!");
    }
}