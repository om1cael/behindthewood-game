using UnityEngine;

public class PlayerMonsterReaction : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip omgSound;

    void Start()
    {
        _audioSource = GlobalResources.Instance.playerAudioSource;
    }

    void OnEnable()
    {
        GameEvents.OnHouseInvaded += PlayOmgSound;
    }

    void OnDisable()
    {
        GameEvents.OnHouseInvaded -= PlayOmgSound;
    }

    void PlayOmgSound() {
        _audioSource.PlayOneShot(omgSound);
    }
}
