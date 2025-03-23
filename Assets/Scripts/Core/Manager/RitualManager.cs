using UnityEngine;

public class RitualManager : MonoBehaviour
{
    private const byte RITUAL_ITEMS_COUNT = 5;
    private byte _playerRitualItemsCount = 0;

    void Start()
    {
        _playerRitualItemsCount = 0;
    }

    void OnEnable()
    {
        GameEvents.OnRitualItemPicked += AddRitualItem;
    }

    void OnDisable()
    {
        GameEvents.OnRitualItemPicked -= AddRitualItem;
    }

    void AddRitualItem() 
    {
        _playerRitualItemsCount++;

        // DEMO LIMIT
        if(_playerRitualItemsCount >= 2) {
            GameEvents.OnHouseInvaded?.Invoke();
        }
        // END

        if(_playerRitualItemsCount >= RITUAL_ITEMS_COUNT) {
            GameEvents.OnRitualItemsCompleted?.Invoke();
        }
    }
}
