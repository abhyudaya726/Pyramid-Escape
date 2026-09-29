using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI interactionPrompt;
    [SerializeField] private UnityEngine.UI.Image crosshair;

    private Camera mainCamera;
    private IInteractable currentInteractable;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("PlayerInteraction: Main Camera not found!");
        }

        HidePrompt();
    }

    private void Update()
    {
        if (mainCamera == null)
            return;

        FindInteractable();
        HandleInteraction();
    }

    private void FindInteractable()
    {
        currentInteractable = null;

        Ray ray = new Ray(
            mainCamera.transform.position,
            mainCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance,
            interactionLayer))
        {
            currentInteractable =
                hit.collider.GetComponentInParent<IInteractable>();
        }

        if (currentInteractable != null)
        {
            ShowPrompt();

            if (crosshair != null)
            {
                crosshair.transform.localScale = Vector3.one * 1.5f;
            }
        }
        else
        {
            HidePrompt();

            if (crosshair != null)
            {
                crosshair.transform.localScale = Vector3.one;
            }
        }
    }

    private void HandleInteraction()
    {
        if (currentInteractable == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    private void ShowPrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(true);
        }
    }

    private void HidePrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }
}