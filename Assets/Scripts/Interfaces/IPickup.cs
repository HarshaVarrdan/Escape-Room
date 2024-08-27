
public interface IPickup
{
    ItemData OnPickup();
    void OnPickedInHand();
    void OnPlaced(bool placedOnDesiredLocation);

    void DisablePickup();
    void EnablePickup();
}
