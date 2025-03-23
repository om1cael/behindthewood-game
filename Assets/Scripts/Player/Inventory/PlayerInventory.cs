using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private bool hammer = false;
    [SerializeField] private int hammerHealth = 0;
    [SerializeField] private int planks = 0;
    [SerializeField] private int nails = 0;
    [Header("Ritual")]
    [SerializeField] public bool key = false;

    private int _hammerDamage = 4;

    void OnEnable()
    {
        GameEvents.OnItemPickup += AddToInventory;
    }

    void OnDisable()
    {
        GameEvents.OnItemPickup -= AddToInventory;
    }

    void AddToInventory(Item item, int amount) 
    {
        switch(item) 
        {
            case Item.PLANK:
                planks += amount;
                GameEvents.OnPlankUpdate?.Invoke(planks);
                break;
            case Item.NAIL:
                nails += amount;
                GameEvents.OnNailUpdate?.Invoke(nails);
                break;
            case Item.HAMMER:
                AddHammer();
                GameEvents.OnAcquireHammer?.Invoke();
                GameEvents.OnHammerHealthChange?.Invoke(hammerHealth);
                break;
            case Item.RITUAL_ITEM:
                GameEvents.OnRitualItemPicked?.Invoke();
                break;
            case Item.KEY_RITUAL_ITEM:
                GameEvents.OnRitualItemPicked?.Invoke();
                AddKey();
                break;
        }
    }

    void DecreaseFromInventory(Item item, int amount) 
    {
        switch(item) 
        {
            case Item.PLANK:
                planks -= amount;
                GameEvents.OnPlankUpdate?.Invoke(planks);
                break;
            case Item.NAIL:
                nails -= amount;
                GameEvents.OnNailUpdate?.Invoke(nails);
                break;
            case Item.HAMMER:
                DecreaseOrRemoveHammer();
                break;
        }
    }

    void AddHammer() {
        hammer = true;
        hammerHealth = 100;
    }

    void DecreaseOrRemoveHammer() {
        hammerHealth -= _hammerDamage;
        
        if(hammerHealth <= 0) {
            hammer = false;
            GameEvents.OnHammerDestroyed?.Invoke();
            return;
        }

        GameEvents.OnHammerHealthChange?.Invoke(hammerHealth);
    }

    public bool UpdateResources(int woods, int nails) {
        if(!HasResources(woods, nails)) return false;

        DecreaseFromInventory(Item.PLANK, woods);
        DecreaseFromInventory(Item.NAIL, nails);
        DecreaseFromInventory(Item.HAMMER, _hammerDamage);
        return true;
    }

    public bool HasResources(int planks, int nails) {
        return hammerHealth > 0 && this.planks >= planks && this.nails >= nails;
    }

    private void AddKey() {
        key = true;
    }

    public bool GetKey() {
        return key;
    }
}
