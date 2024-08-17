using Flexalon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ReArrangeBook : MonoBehaviour , IInteract
{
    [SerializeField] Transform parentTrans;
    [SerializeField] Camera bookCamera;
    [SerializeField] Sprite chImage;
    [SerializeField] Sprite chImageN;
    [SerializeField] Drawer toUnlockDrawer;

    bool canRearrange;
    bool canInteract = true;
    bool isInteracting = false;

    private Camera mainCamera;

    public UnityAction<bool> onRearrangeStart;
    public UnityAction<bool> onRearrangeEnd;

    public static ReArrangeBook AB_Instance;

    private void Awake()
    {
        if (AB_Instance == null) {
            AB_Instance = this;
            Debug.Log("Arrange Book Instance Created");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in parentTrans)
        {
            //child.GetComponent<FlexalonObject>().enabled = false;
            child.GetComponent<FlexalonInteractable>().enabled = false;
            child.GetComponent<BoxCollider>().enabled = false;
        }
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isInteracting)
        {
            EndInteraction();
        }
    }

    public void OnInteraction()
    {
        int itemIndex = PlayerController.PC_Instance.indexItemInHand;
        if (!canRearrange && !CanStartRearrange())
        {
            if (itemIndex >= 0)
            {
                ItemData itemData = PlayerInventory.PIn_Instance.GetItemData(itemIndex);
                if (itemData.itemName == "Book")
                {
                    Debug.Log("Book Added");
                    GameObject book = PlayerInventory.PIn_Instance.GetItemGO(itemIndex);
                    book.transform.SetParent(parentTrans);
                    book.GetComponent<Pickup>().OnPlaced();
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

    public void EndInteraction()
    {
        EndRearranging(false);
    }

    public bool CanStartRearrange()
    {
        if(parentTrans.childCount == 10)
        {
            canRearrange = true;

            ChangeBookInteractionStatus(true);
            
            return true;
        }
        return false;
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
        EndRearranging(true);

    }

    private void StartRearranging()
    {
        mainCamera.enabled = !canRearrange;
        bookCamera.enabled = canRearrange;
        
        isInteracting = true;
        ChangeBookInteractionStatus(true);

        onRearrangeStart?.Invoke(false);
    }

    private void EndRearranging(bool val)
    {
        if (val)
        {
            toUnlockDrawer.ChangeInteractionState(true);
            toUnlockDrawer.OnInteraction();

            canRearrange = false;
            canInteract = false;
        }

        isInteracting = false;

        ChangeBookInteractionStatus(false);

        mainCamera.enabled = true;
        bookCamera.enabled = false;

        onRearrangeEnd?.Invoke(true);
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public Sprite GetInteractImage()
    {
        return (canInteract) ? chImage : chImageN;
    }

    private void ChangeBookInteractionStatus(bool val)
    {
        foreach (Transform child in parentTrans)
        {
            child.GetComponent<BoxCollider>().enabled = val;
            child.GetComponent<FlexalonInteractable>().enabled = val;
        }
    }
}
