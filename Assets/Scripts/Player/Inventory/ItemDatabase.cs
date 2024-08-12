using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDatabase", menuName = "ScriptableObjects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> allItems;

    /*public ItemData GetItemByID(int id)
    {
        return allItems.Find(item => item.itemName == id);
    }*/

    public ItemData GetItemByName(string name)
    {
        return allItems.Find(item => item.itemName == name);
    }
}
