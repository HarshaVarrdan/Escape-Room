using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour, IInteract
{

    [SerializeField] bool isOpened = false;
    [SerializeField] bool isZAxis = false;
    [SerializeField] bool canInteract = true;
    
    [SerializeField] float offset;
    
    [SerializeField] Sprite chImage;    
    [SerializeField] Sprite chImageN;

    [SerializeField] AudioClip onInteractSound;
    [SerializeField] AudioClip onNoInteractSound;

    private AudioSource interactionAS;


    // Start is called before the first frame update
    void Start()
    {
        interactionAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaySound(bool interacted)
    {
        if (interactionAS != null)
            if (interacted)
                interactionAS.PlayOneShot(onInteractSound);
            else
                interactionAS.PlayOneShot(onNoInteractSound);

    }

    public void OnInteraction()
    {
        if (!isOpened)
        {
            Debug.Log("Called");
            if (isZAxis)
                transform.position -= transform.up * offset;
            else
                transform.position += transform.right * offset;
            isOpened = true;
        }
        else if (isOpened)
        {
            EndInteraction();
        }
    }

    public void EndInteraction()
    {
        Debug.Log("Called");
        if (isZAxis)
            transform.position += transform.up * offset;
        else
            transform.position -= transform.right * offset;
        isOpened = false;
    }

    public void ChangeInteractionState(bool val)
    {
        canInteract = val;
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public Sprite GetInteractImage()
    {
        if(canInteract)
            return chImage;
        else
            return chImageN;
    }
}
