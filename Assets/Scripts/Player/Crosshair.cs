using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{

    [SerializeField] float rayDistance;
    [SerializeField] Sprite interactCH;
    [SerializeField] Sprite defaultCH;

    private Image crosshairImage;

    bool crosshairStatus = true;

    public static Crosshair crosshair;

    private void Awake()
    {
        if (crosshair == null)
        {
            crosshair = this;
            Debug.Log("Crosshair Instance Created");

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        crosshairImage = GetComponent<Image>();
        ReArrangeBook.AB_Instance.onRearrangeEnd += HandleCrosshairStatus;
        ReArrangeBook.AB_Instance.onRearrangeStart += HandleCrosshairStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if (crosshairStatus)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteract interact))
                {
                    ChangeCrossHair(interact.GetInteractImage());
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Interacted");
                        PlayerInteractions.PI_Instance.initInteraction(interact);
                    }
                }
                else if (hit.collider.gameObject.TryGetComponent(out IPickup pickup))
                {
                    ChangeCrossHair(interactCH);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Picked Up");
                        PlayerInteractions.PI_Instance.initPickup(hit.collider.gameObject);
                    }
                }
                else
                    ChangeCrossHair(defaultCH);

            }
        }
    }

    public void ChangeCrossHair(Sprite image)
    {
        crosshairImage.sprite = image;
    }

    private void HandleCrosshairStatus(bool val)
    {
        crosshairStatus = val;
        crosshairImage.enabled = val;
    }

}
