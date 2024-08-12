using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteract
{

    [SerializeField] bool isOpened = false;
    [SerializeField] bool isZAxis = false;
    [SerializeField] float offset;

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
        if (!isOpened)
        {
            Debug.Log("Called");
            if(isZAxis) 
                transform.position -= transform.up * offset;
            else
            transform.position += transform.right * offset;
            isOpened = true;
        }
        else if (isOpened) 
        {
            Debug.Log("Called");
            if (isZAxis)
                transform.position += transform.up * offset;
            else
                transform.position -= transform.right * offset;
            isOpened = false;
        }
    }
}
