using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    PlayerInventory pInventory;

    public static PlayerInteractions PI_Instance;

    private void Awake()
    {
        if(PI_Instance == null)
        {
            PI_Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pInventory =  GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initInteraction(IInteract interact)
    {
        interact.OnInteraction();
    }

    public void initPickup(GameObject pickupGO)
    {
        ItemData id = pickupGO.GetComponent<IPickup>().OnPickup();
        if (id != null)
        {
            pInventory.AddItem(id,pickupGO);
            //GetComponent<PlayerController>().AddObjectToHand(pickup.)
        }

    }

}
