using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float upDownRange = 60f;
    public bool canRotate = true;
    public bool canMove = true;
    
    public ItemData itemDataOfItemInHand = null;
    public GameObject gameObjectOfItemInHand = null;
    public PickableItem pickableItemInHand = null;

    [SerializeField] GameObject ItemHolder;

    private float verticalRotation = 0f;

    private CharacterController characterController;
    private Camera playerCamera;

    public UnityAction playerDisableInteraction;

    public static PlayerController PC_Instance;

    private void Awake()
    {
        if (PC_Instance == null)
        {
            PC_Instance = this;
            Debug.Log("Player Controller Instance Created");

        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        ReArrangeBook.AB_Instance.onRearrangeStart += ChangeCanMoveRotateValue;
        ReArrangeBook.AB_Instance.onRearrangeEnd += ChangeCanMoveRotateValue;
    }

    void Update()
    {
        if (canMove)
        {
            // Handle Player Movement
            float moveDirectionY = 0f;
            float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed;
            float moveDirectionZ = Input.GetAxis("Vertical") * moveSpeed;

            Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;
            characterController.Move(move * Time.deltaTime);
        }
        if (canRotate)
        {
            // Handle Player Rotation
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ItemData itemData = itemDataOfItemInHand;
            if (itemData != null)
            {
                PlayerInventory.PIn_Instance.RemoveItem(itemData,false);
            }

        }
    }

    void ChangeCanMoveRotateValue(bool val)
    {
        canRotate = val; 
        canMove = val;
        playerDisableInteraction?.Invoke();
    }

    void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        Transform spawnpointTransform = GameObject.FindWithTag("SpawnPoint").gameObject.transform;
        if(spawnpointTransform != null ) 
            transform.SetPositionAndRotation(spawnpointTransform.position,spawnpointTransform.rotation);
    }

    void ChangeCanMoveValue(bool val)
    {
        canMove = val;
    }

    public void TakeObjectInHand(ItemData id)
    {
        
        if (ItemHolder.transform.childCount > 0)
        {
            foreach (Transform child in ItemHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (id == null)
            return;
        
        itemDataOfItemInHand = id;
        gameObjectOfItemInHand = Instantiate(itemDataOfItemInHand.itemObject,ItemHolder.transform);
        pickableItemInHand = gameObjectOfItemInHand.GetComponent<PickableItem>();
        
        pickableItemInHand.OnPickedInHand();

    }

    public void RemoveObjectFromHand(bool dropItem)
    {
        if (gameObjectOfItemInHand != null)
        {
            if (dropItem)
                DropItemfromHand();
            Destroy(gameObjectOfItemInHand);
        }
        gameObjectOfItemInHand = null;
        itemDataOfItemInHand = null;
        pickableItemInHand = null;
    }

    void DropItemfromHand()
    {
        GameObject temp = Instantiate(gameObjectOfItemInHand, ItemHolder.transform.position, Quaternion.identity);
        temp.GetComponent<PickableItem>().OnPlaced(false);
    }
}
