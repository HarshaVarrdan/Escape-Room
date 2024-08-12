using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
    public List<GameObject> itemsGO = new List<GameObject>();
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

    public ItemData GetItem(int index)
    {
        return items[index];
    }

    public GameObject GetItemGO(int index)
    {
        return itemsGO[index];
    }
}


