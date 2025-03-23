using UnityEngine;

public class CandleMission : MonoBehaviour, IInteractable
{
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isActive;

    [SerializeField] private AudioClip openingDrawer;
    [SerializeField] private AudioClip lockedDrawer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _isActive = true;
    }

    public void Interact()
    {
        if(!GlobalResources.Instance.playerInventory.GetKey()) {
            _audioSource.PlayOneShot(lockedDrawer);
            return;
        }

        _audioSource.PlayOneShot(openingDrawer);
        _animator.enabled = true;
        _isActive = false;
    }

    public bool AvailableForInteraction()
    {
        return _isActive;
    }

    public string GetName()
    {
        return "Drawer";
    }
}
