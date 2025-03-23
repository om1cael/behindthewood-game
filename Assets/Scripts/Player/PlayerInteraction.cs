using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickableInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private float maxInteractionDistance;
    [SerializeField] private InputActionReference interactionInput;

    private Camera _playerCamera;
    private string _previousItem;

    void Start()
    {
        _playerCamera = Camera.main;
    }

    void Update()
    {
        if(Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, maxInteractionDistance, interactableLayerMask)) {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if(interactable == null || !interactable.AvailableForInteraction()) return;

            if(_previousItem != interactable?.GetName()) {
                GameEvents.OnInteractableHighlighted?.Invoke();
            }

            if(interactionInput.action.triggered) {
                interactable?.Interact();
            }
        }
        else if(hit.collider == null && _previousItem != null) {
            GameEvents.OnInteractableHighlighted?.Invoke();
            _previousItem = null;
        }
    }
}
