using System;

public interface IInteractable
{
    public void Interact();
    public String GetName();
    public bool AvailableForInteraction();
}
