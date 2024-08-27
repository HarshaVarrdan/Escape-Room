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

    private void ChangeBookInteractionStatus(bool val)
    {
        foreach (Transform child in parentTrans)
        {
            child.GetComponent<BoxCollider>().enabled = val;
            child.GetComponent<FlexalonInteractable>().enabled = val;

            child.GetComponent<FlexalonInteractable>().DragEnd.AddListener(OnBookOrderChanged);
        }
    }

    public void OnInteraction()
    {
        ItemData itemData = PlayerController.PC_Instance.itemDataOfItemInHand;
        if (!canRearrange && !CanStartRearrange())
        {
            if (itemData != null)
            {
                if (itemData.itemName == "Book")
                {
                    Debug.Log("Book Added");

                    GameObject book = Instantiate(itemData.itemObject,parentTrans);
                    book.GetComponent<PickableItem>().OnPlaced(true);
                    book.GetComponent<PickableItem>().DisablePickup();
                    book.GetComponent<BoxCollider>().enabled = false;
                    Destroy(book.GetComponent<Rigidbody>());

                    PlayerInventory.PIn_Instance.RemoveItem(itemData,true);

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

    public bool CanInteract()
    {
        return canInteract;
    }

    public Sprite GetInteractImage()
    {
        return (canInteract) ? chImage : chImageN;
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

    public void OnBookOrderChanged(FlexalonInteractable fi)
    {
        List<PickableItem> books = new List<PickableItem>();
        books.AddRange(GetComponentsInChildren<PickableItem>());

        int i = 10;

        foreach (PickableItem book in books) 
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

}
