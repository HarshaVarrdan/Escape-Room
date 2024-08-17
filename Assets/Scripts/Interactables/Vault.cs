using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour, IInteract
{

    bool isOpened;

    bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public void OnInteraction()
    {
        if (!isOpened)
        {
            Debug.Log("Called");
            isOpened = true;
        }
        else if (isOpened)
        {
            EndInteraction();
        }
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public Sprite GetInteractImage()
    {
        throw new System.NotImplementedException();
    }


}
