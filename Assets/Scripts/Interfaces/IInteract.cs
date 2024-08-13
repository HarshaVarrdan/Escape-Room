using UnityEngine;

public interface IInteract 
{
    void OnInteraction();
    void EndInteraction();
    bool CanInteract();
    Sprite GetInteractImage();
}
