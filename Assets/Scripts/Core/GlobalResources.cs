using UnityEngine;

public class GlobalResources : MonoBehaviour
{
    public static GlobalResources Instance;

    [Header("Audio")]
    public AudioClip doorOpening;
    public AudioClip doorClosing;
    [Header("Placeables")]
    public GameObject plankPrefab;
    public PlayerInventory playerInventory;

    [Header("Item Pickup")]
    public AudioSource playerAudioSource;
    public AudioClip itemPickupClip;
    public AudioClip noteReadClip;

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
