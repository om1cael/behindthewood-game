using System;

public class GameEvents
{
    // Interactions
    public static Action OnInteractableHighlighted;
    public static Action<Item, int> OnItemPickup;

    // Inventory
    public static Action<int> OnPlankUpdate;
    public static Action<int> OnNailUpdate;
    public static Action OnAcquireHammer;
    public static Action OnHammerDestroyed;
    public static Action<int> OnHammerHealthChange;

    public static Action OnInsufficientResources;

    // Notes
    public static Action<string> OnReadNote;
    public static Action OnNoteClosed;

    // House
    public static Action OnHouseEntered;
    public static Action OnHouseInvaded;

    // Ritual
    public static Action OnRitualItemPicked;
    public static Action OnRitualItemsCompleted;
}