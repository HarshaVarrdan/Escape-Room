using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] List<ItemData> items = new List<ItemData>(8);

    public UnityAction<List<ItemData>> addItemEvent;
    public UnityAction<List<ItemData>> removeItemEvent;

    private static Inventory instance;
    public static Inventory Inv_Instance
    {
        get {
            if (instance == null)
            {
                instance = Resources.Load<Inventory>("Objects/Inventory");
                if (instance == null)
                {
                    Debug.LogError("Singleton instance of MyScriptableSingleton not found. Please create it and place it in a Resources folder.");
                }
            }
            return instance;
        }
    }

    
    public void AddItem(ItemData item)
    {
        items.Add(item);
        addItemEvent?.Invoke(items);
    }

    public void RemoveItem(ItemData id)
    {
        if (items.Contains(id))
        {
            items.Remove(id);
            removeItemEvent?.Invoke(items);
        }
    }

    public void RemoveItem(int index)
    {
        if (items[index] != null)
        { 
            items.RemoveAt(index);
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

    public bool CheckForItem(ItemData id)
    {
        return items.Contains(id);
    }

    public void ClearInventory()
    {
        items.Clear();
    }
}

[CustomEditor(typeof(Inventory))]
public class MyInventoryObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Inventory myInventoryObject = (Inventory)target;

        if (GUILayout.Button("Clear Inventory"))
        {
            Debug.Log("Button pressed!");
            myInventoryObject.ClearInventory();
        }
    }
}


