using Flexalon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ReArrangeBook : MonoBehaviour , IInteract
{

    [SerializeField] Camera bookCamera;

    bool canRearrange;

    public UnityAction onRearrangeStart;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            //child.GetComponent<FlexalonObject>().enabled = false;
            child.GetComponent<FlexalonInteractable>().enabled = false;
            child.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteraction()
    {
        int itemIndex = PlayerController.PC_Instance.indexItemInHand;
        if (!canRearrange)
        {
            if (itemIndex >= 0)
            {
                ItemData itemData = PlayerInventory.PIn_Instance.GetItemData(itemIndex);
                if (itemData.itemName == "Book")
                {
                    Debug.Log("Book Added");
                    GameObject book = PlayerInventory.PIn_Instance.GetItemGO(itemIndex);
                    book.transform.SetParent(transform);
                    book.GetComponent<Book>().OnPlaced();
                    PlayerInventory.PIn_Instance.RemoveItem(itemIndex);
                    PlayerController.PC_Instance.indexItemInHand = -1;
                    CanStartRearrange();
                }
                else
                {
                    Debug.Log("Nothing Added");
                }
            }
        }
        else
        {
            StartRearranging();
        }
    }

    public void CanStartRearrange()
    {
        if(transform.childCount == 10)
        {
            canRearrange = true;
            
            foreach (Transform child in transform) 
            {
                child.GetComponent<BoxCollider>().enabled = true;
                child.GetComponent<FlexalonInteractable>().enabled = true;
            }
        }
    }

    public void OnBookOrderChanged()
    {
        List<Book> books = new List<Book>();
        books.AddRange(GetComponentsInChildren<Book>());

        int i = 10;

        foreach (Book book in books) 
        {
            if(book.GetBookNumber() != i)
            {
                Debug.Log($"Arrangement wrong {book.GetBookNumber()} {i}");
                return;
            }
            i--;
        }
        Debug.Log("Arrangement Correct");

    }

    private void StartRearranging()
    {
        Camera.main.enabled = !canRearrange;
        bookCamera.enabled = canRearrange;

        onRearrangeStart?.Invoke();
    }

}
