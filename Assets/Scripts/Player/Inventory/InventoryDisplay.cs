using Rito.RadialMenu_v3;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class InventoryDisplay : MonoBehaviour
{

    [SerializeField] Inventory inventory;
    [SerializeField] RadialMenu radialMenu;

    [SerializeField] GameObject uiParent;
    [SerializeField] GameObject itemFrame;

    PlayerController playerController;

    public static UnityAction onRadialMenuOpen;
    public static UnityAction onRadialMenuClose;

    public static InventoryDisplay inventoryDisplay;

    private void Awake()
    {
        if (inventoryDisplay == null)
        {
            inventoryDisplay = this;
            Debug.Log("Inventory Display Instance Created");

        }

        inventory = Resources.Load<Inventory>("Objects/Inventory");

        inventory.addItemEvent += HandleInventoryChanges;
        inventory.removeItemEvent += HandleInventoryChanges;

    }

    // Start is called before the first frame update
    void Start()
    {
        HandleInventoryChanges(inventory.GetItems());
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            radialMenu.Show();
            playerController.canRotate = false;
            onRadialMenuOpen?.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            int selected = radialMenu.Hide();
            Debug.Log($"Selected : {selected}");
            if (inventory.GetItems().Count - 1 >= selected)
            {
                Debug.Log($"Selected : {selected} {inventory.GetItem(selected).itemName}");
                playerController.TakeObjectInHand(inventory.GetItems()[selected]);
            }
            else
            {
                Debug.Log($"Selected : {selected}");
                selected = -1;
                playerController.TakeObjectInHand(null);
            }
            playerController.canRotate = true;
            onRadialMenuClose?.Invoke();
        }
    }

    void HandleInventoryChanges(List<ItemData> itemsData)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (ItemData item in itemsData)
        {
            GameObject iFrame = Instantiate(itemFrame, transform);
            iFrame.GetComponent<ItemFrameUI>().id = item;
            
        }
        radialMenu.SetPieceImageSprites(itemsData);
    }
}
