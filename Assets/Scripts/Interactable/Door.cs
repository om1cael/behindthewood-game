using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isEntranceDoor;
    private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool isLocked;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool AvailableForInteraction()
    {
        return !isLocked;
    }

    public string GetName()
    {
        return "Door";
    }

    public void Interact()
    {
        animator.SetTrigger("Toggle");

        if(isOpen) {
            audioSource.resource = GlobalResources.Instance.doorClosing;
        } else {
            audioSource.resource = GlobalResources.Instance.doorOpening;
        }

        audioSource.Play();
        isOpen = !isOpen;
        
        if(isEntranceDoor) {
            isLocked = true;
        }
    }
}
