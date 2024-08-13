using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour, IInteract
{

    [SerializeField] bool isOpened = false;
    [SerializeField] bool isZAxis = false;
    [SerializeField] float offset;
    [SerializeField] Sprite chImage;    
    [SerializeField] Sprite chImageN;


    public bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteraction()
    {
        if (canInteract)
        {
            if (!isOpened)
            {
                Debug.Log("Called");
                if (isZAxis)
                    transform.position -= transform.up * offset;
                else
                    transform.position += transform.right * offset;
                isOpened = true;
            }
            else if (isOpened)
            {
                EndInteraction();
            }
        }
    }

    public void EndInteraction()
    {
        Debug.Log("Called");
        if (isZAxis)
            transform.position += transform.up * offset;
        else
            transform.position -= transform.right * offset;
        isOpened = false;
    }

    public void ChangeInteractionState(bool val)
    {
        canInteract = val;
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public Sprite GetInteractImage()
    {
        if(canInteract)
            return chImage;
        else
            return chImageN;
    }

}
