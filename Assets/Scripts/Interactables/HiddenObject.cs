using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour, IInteract
{ 

    [SerializedDictionary("GameObject", "Count")]
    public SerializedDictionary<GameObject, int> hiddenObjects = new SerializedDictionary<GameObject,int>();
    //public Dictionary<ItemData, GameObject> hiddenObjects = new Dictionary<ItemData, GameObject>();

    private bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemPicked()
    {

    }

    public void OnInteraction()
    {
        
    }

    public void EndInteraction()
    {
        canInteract = false;
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public Sprite GetInteractImage()
    {
        throw new System.NotImplementedException();
    }
}
