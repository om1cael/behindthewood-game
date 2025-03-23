using System;
using UnityEngine;

public class Pickable : MonoBehaviour, IInteractable
{
    [SerializeField] private PickableSO pickableData;

    private AudioSource _audioSource;
    private AudioClip _pickupClip;

    void Start()
    {
        _audioSource = GlobalResources.Instance.playerAudioSource;
        _pickupClip = GlobalResources.Instance.itemPickupClip;
    }

    public void Interact()
    {
        GameEvents.OnItemPickup?.Invoke(pickableData.item, pickableData.amount);
        _audioSource.PlayOneShot(_pickupClip);
        Destroy(gameObject);
    }

    public String GetName() {
        return pickableData.name;
    }

    public bool AvailableForInteraction()
    {
        return true;
    }
}
