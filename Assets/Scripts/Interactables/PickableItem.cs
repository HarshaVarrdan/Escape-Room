using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickup
{
    [SerializeField] bool canBePicked;
    [SerializeField] Vector3 inHandRotation;
    [SerializeField] Vector3 onPlacedOnDesiredLocationRotation;
    [Header("Item Data")]
    [SerializeField] ItemData itemData;

    [SerializeField] Rigidbody itemRB;
    bool isPickedUp;

    //Book Variables
    [Header("Book Variables")]
    [SerializeField] bool isBook;
    [SerializeField] int bookNumber;
    [SerializeField] TMP_Text numText;

    //Key Variable
    [Header("Key Variables")]
    [SerializeField] bool isKey;

    // Start is called before the first frame update
    void Start()
    {
        if (isBook)
            Book();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Book()
    {
        numText.text = bookNumber.ToString();
    }

    public int GetBookNumber()
    {
        if(isBook) 
            return bookNumber;
        return -1;
    }

    public ItemData OnPickup()
    {
        if (!isPickedUp && canBePicked)
        {
            Destroy(this.gameObject,.1f);
            return itemData;
        }
        return null;
    }

    public void OnPickedInHand()
    {
        gameObject.layer = 8;

        transform.localRotation = Quaternion.Euler(inHandRotation);
        transform.localPosition = Vector3.zero;

        itemRB.useGravity = false;
        itemRB.isKinematic = true;

        isPickedUp = true;

        Debug.Log($"{gameObject.name} has been Pickedr Up in Hand");
    }

    public void OnPlaced(bool placedOnDesiredLocation)
    {
        if (placedOnDesiredLocation)
        {
            transform.localRotation = Quaternion.Euler(onPlacedOnDesiredLocationRotation);
            return;
        }

        itemRB.useGravity = true;
        itemRB.isKinematic = false;

        canBePicked = true;
        isPickedUp = false;
    }

    public void DisablePickup()
    {
        canBePicked = false;
    }

    public void EnablePickup()
    {
        canBePicked = true;
    }
}
