using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] Inventory inventory;

    public UnityAction onRemoveItem;
    public UnityAction onAddItem;

    private PlayerController playerController;

    public static PlayerInventory PIn_Instance;

    private void Awake()
    {
        if (PIn_Instance == null)
        {
            PIn_Instance = this;
            Debug.Log("Player Inventory Instance Created");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemData item)
    {
        inventory.AddItem(item);

        onAddItem?.Invoke();
    }

    public void RemoveItem(ItemData id, bool destroyItem)
    {
        inventory.RemoveItem(id);
        playerController.RemoveObjectFromHand(!destroyItem);
        onRemoveItem?.Invoke();
    }

    /*public void DropItem(int index)
    {
        GameObject goToRemove = inventory.GetItemGO(index);
        if (goToRemove != null)
        {
            inventory.RemoveItem(index);
            goToRemove.GetComponent<IPickup>().OnPlaced(false);
            onRemoveItem?.Invoke(goToRemove);
        }
    }*/

    public ItemData GetItemData(int index)
    {
        return inventory.GetItem(index);
    }

    public bool checkItem(ItemData id) 
    {
        return inventory.CheckForItem(id);
    }

}
