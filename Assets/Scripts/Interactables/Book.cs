using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Book : MonoBehaviour, IPickup
{

    [SerializeField] ItemData itemData;
    [SerializeField] TMP_Text numText;
    
    [SerializeField] int bookNumber;
    [SerializeField] bool canBePicked;
    [SerializeField] Vector3 inHandRotation;
    [SerializeField] Vector3 onPlacedRotation;

    bool isPickedUp = false;


    // Start is called before the first frame update
    void Start()
    {
        numText.text = bookNumber.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemData OnPickup()
    {
        if (!isPickedUp && canBePicked)
        {
            isPickedUp = true;
            PlayerController.PC_Instance.AddObjectToHand(this.gameObject);
            return itemData;
        }
        return null;
    }

    public void OnPickedInHand()
    {
        transform.localRotation = Quaternion.Euler(inHandRotation);
        transform.localPosition = Vector3.zero;
    }

    public void OnPlaced()
    {
        transform.localRotation = Quaternion.Euler(onPlacedRotation);
    }

    public int GetBookNumber()
    {
        return bookNumber;
    }
}
