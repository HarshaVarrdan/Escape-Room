using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{

    [SerializeField] Sprite interactCH;
    [SerializeField] Sprite defaultCH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200f))
        {
            if(hit.collider.gameObject.TryGetComponent(out IInteract interact))
            {
                Debug.Log("Interact Item Found");
                GetComponent<Image>().sprite = interactCH;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Interacted");
                    PlayerInteractions.PI_Instance.initInteraction(interact);
                }
            }
            else if (hit.collider.gameObject.TryGetComponent(out IPickup pickup))
            {
                Debug.Log("PickUp Item Found");
                GetComponent<Image>().sprite = interactCH;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Picked Up");
                    PlayerInteractions.PI_Instance.initPickup(hit.collider.gameObject);
                }
            }
            else
                GetComponent<Image>().sprite = defaultCH;

        }
    }
}
