using UnityEngine;

public class IntroInteraction : MonoBehaviour, IMission
{
    [SerializeField] private GameObject door;

    public void StartMission()
    {
        //IInteractable doorInteractable = door.GetComponent<IInteractable>();
        //doorInteractable?.Interact();

        GameEvents.OnHouseEntered?.Invoke();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
            StartMission();
        }
    }
}
