using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractable
{
    [Header("Lever Settings")]
    [SerializeField] private float onAngle = -45f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Events")]
    [SerializeField] private UnityEvent onLeverActivated;
    [SerializeField] private UnityEvent onLeverDeactivated;

    private Quaternion offRotation;
    private Quaternion onRotation;

    private bool isOn;

    private void Start()
    {
        offRotation = transform.localRotation;

        onRotation =
            offRotation *
            Quaternion.Euler(onAngle, 0f, 0f);
    }

    private void Update()
    {
        Quaternion targetRotation =
            isOn ? onRotation : offRotation;

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    public void Interact()
    {
        isOn = !isOn;

        if (isOn)
        {
            Debug.Log("Lever turned ON!");
            onLeverActivated?.Invoke();
        }
        else
        {
            Debug.Log("Lever turned OFF!");
            onLeverDeactivated?.Invoke();
        }
    }
}