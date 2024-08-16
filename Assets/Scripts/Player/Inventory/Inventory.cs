using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    List<ItemData> items = new List<ItemData>(8);
    List<GameObject> itemsGO = new List<GameObject>(8);
    public UnityAction<List<ItemData>> addItemEvent;
    public UnityAction<List<ItemData>> removeItemEvent;

    public void AddItem(ItemData item, GameObject itemGO)
    {
        items.Add(item);
        itemsGO.Add(itemGO);
        addItemEvent?.Invoke(items);
    }

    public void RemoveItem(int index)
    {
        if (items[index] != null && itemsGO[index] != null)
        { 
            items.RemoveAt(index);
            itemsGO.RemoveAt(index);
            removeItemEvent?.Invoke(items);
        }
    }

    public List<ItemData> GetItems()
    {
        return items; 
    }

    public List<GameObject> GetItemsGO()
    {
        return itemsGO;
    }

    public ItemData GetItem(int index)
    {
        return items[index];
    }

    public GameObject GetItemGO(int index)
    {
        return itemsGO[index];
    }

    public bool CheckForItem(GameObject itemGO)
    {
        return itemsGO.Contains(itemGO);
    }
}


