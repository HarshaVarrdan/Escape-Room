using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    PlayerInventory pInventory;
    PlayerController pController;

    public static PlayerInteractions PI_Instance;

    private void Awake()
    {
        if(PI_Instance == null)
        {
            PI_Instance = this;
            Debug.Log("Player Interaction Instance Created");

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pInventory =  GetComponent<PlayerInventory>();
        pController = GetComponent<PlayerController>();
    }

    public void initInteraction(IInteract interact)
    {
        if (interact.CanInteract())
            interact.OnInteraction();
    }

    public void initPickup(GameObject pickupGO)
    {
        ItemData id = pickupGO.GetComponent<IPickup>().OnPickup();
        if (id != null)
        {
            Debug.Log($"{id.itemName} has been Picked up");
            pInventory.AddItem(id);
            pController.TakeObjectInHand(id);
        }

        Debug.Log($"{pickupGO.name} has no Item Data");

    }

}
