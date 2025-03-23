using TMPro;
using UnityEngine;

public class Readable : MonoBehaviour, IInteractable, IReadable
{
    [SerializeField] private Missions mission;
    [SerializeField] private string text;

    private AudioSource _audioSource;
    private AudioClip _readClip;

    void Start()
    {
        _audioSource = GlobalResources.Instance.playerAudioSource;
        _readClip = GlobalResources.Instance.noteReadClip;
    }

    public void Interact()
    {
        if(text == null || text.Length <= 0) return;
        GameEvents.OnReadNote?.Invoke(text);

        _audioSource.PlayOneShot(_readClip);
    }

    public bool AvailableForInteraction()
    {
        return true;
    }

    public string GetName()
    {
        return "Note";
    }

    public Missions GetMission()
    {
        return mission;
    }
}
