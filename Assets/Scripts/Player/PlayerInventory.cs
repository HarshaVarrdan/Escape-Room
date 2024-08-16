using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] Inventory inventory;

    public UnityAction onRemoveItem;
    public UnityAction onAddItem;

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
        foreach(GameObject item in inventory.GetItemsGO())
        {
            GetComponent<PlayerController>().AddObjectToHand(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemData item, GameObject go)
    {
        inventory.AddItem(item,go);
    }

    public void RemoveItem(int index)
    {
        inventory.RemoveItem(index);
    }

    public ItemData GetItemData(int index)
    {
        return inventory.GetItem(index);
    }

    public GameObject GetItemGO(int index)
    {
        return inventory.GetItemGO(index);
    }

    public bool checkItem(GameObject go) 
    {
        return inventory.CheckForItem(go);
    }
}
