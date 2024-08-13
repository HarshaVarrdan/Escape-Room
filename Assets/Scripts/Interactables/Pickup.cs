using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IPickup
{

    [SerializeField] ItemData itemData;

    [SerializeField] bool canBePicked;
    [SerializeField] Vector3 inHandRotation;
    [SerializeField] Vector3 onPlacedRotation;

    bool isPickedUp = false;

    public ItemData OnPickup()
    {
        if (!isPickedUp && canBePicked)
        {
            isPickedUp = true;
            PlayerController.PC_Instance.AddObjectToHand(this.gameObject);
            return itemData;
        }
        return null;
    }

    public void OnPickedInHand()
    {
        transform.localRotation = Quaternion.Euler(inHandRotation);
        transform.localPosition = Vector3.zero;
    }

    public void OnPlaced()
    {
        transform.localRotation = Quaternion.Euler(onPlacedRotation);
    }

}
