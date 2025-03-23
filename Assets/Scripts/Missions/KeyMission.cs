using UnityEngine;

public class KeyMission : MonoBehaviour, IInteractable
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _isActive;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fallingSound;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _isActive = true;
    }

    public void Interact()
    {
        _rigidbody.isKinematic = false;
        _animator.enabled = false;
        _isActive = false;

        audioSource.Stop();
        audioSource.PlayOneShot(fallingSound);
    }

    public bool AvailableForInteraction()
    {
        return _isActive;
    }

    public string GetName()
    {
        return "KeyMission";
    }
}
