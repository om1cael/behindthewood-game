using UnityEngine;

public class Placeable : MonoBehaviour, IInteractable, IDamageable
{
    private enum Angles {Zero = 0, Ninety = 90};

    [Header("Placement Settings")]
    [SerializeField] private Transform[] placementPoints;
    [SerializeField] private PlaceableSO placeableData;
    private PlayerInventory playerInventory;
    private GameObject woodPrefab;
    [SerializeField] private Angles angle;
    
    [Header("Damage Settings")]
    [SerializeField] private byte life;
    [SerializeField] private bool isProtected = false;

    void Start()
    {
        playerInventory = GlobalResources.Instance.playerInventory;
        woodPrefab = GlobalResources.Instance.plankPrefab;
    }

    public void Interact()
    {
        int screwAmount = placeableData.woodAmount * 2;

        if(!playerInventory.UpdateResources(placeableData.woodAmount, screwAmount)) {
            GameEvents.OnInsufficientResources?.Invoke(); 
            return;
        }

        if(!isProtected) {
            foreach(Transform point in placementPoints) {
                Instantiate(woodPrefab, point.position, Quaternion.Euler(0, (int) angle, 0));
            }
        }

        life = 100;
        isProtected = true;
    }

    public string GetName()
    {
        return placeableData.name;
    }

    public bool AvailableForInteraction()
    {
        return !isProtected || life < 100;
    }

    public bool IsProtected()
    {
        return isProtected;
    }

    public void Damage(byte amount)
    {
        life -= amount;

        if(life <= 0) {
            isProtected = false;
        }
    }
}