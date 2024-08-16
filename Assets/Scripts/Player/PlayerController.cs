using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float upDownRange = 60f;
    public bool canRotate = true;
    public bool canMove = true;
    
    public int indexItemInHand = -1;

    [SerializeField] GameObject ItemHolder;

    private float verticalRotation = 0f;

    private CharacterController characterController;
    private Camera playerCamera;

    List<GameObject> itemsInHand = new List<GameObject>();

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
    }

    void ChangeCanMoveRotateValue(bool val)
    {
        canRotate = val; 
        canMove = val;
    }

    void ChangeCanMoveValue(bool val)
    {
        canMove = val;
    }

    public void TakeObjectInHand(int index)
    {
        if (ItemHolder.transform.childCount > 0)
        {
            foreach (Transform child in ItemHolder.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        if (index >= 0)
        {
            ItemHolder.transform.GetChild(index).gameObject.SetActive(true);
        }
        indexItemInHand = index;
    }

    public void AddObjectToHand(GameObject pickedObject)
    {
        itemsInHand.Add(pickedObject);
        pickedObject.transform.parent = ItemHolder.transform;
        pickedObject.GetComponent<IPickup>().OnPickedInHand();  
    }

    public void RemoveObjectFromHand(GameObject pickedObject)
    {
        itemsInHand.Remove(pickedObject);
    }
}
