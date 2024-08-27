using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour , IInteract
{

    [SerializeField] int DoorSceneIndex;
    [SerializeField] bool isReturnDoor;

    [SerializeField] ItemData keyObject;
    [SerializeField] Sprite chImage;
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
        if(interactionAS != null)
            if(interacted)
                interactionAS.PlayOneShot(onInteractSound);
            else
                interactionAS.PlayOneShot(onNoInteractSound);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(DoorSceneIndex);
    }

    public bool CanInteract()
    {
        return true;
    }

    public void OnInteraction()
    {
        if (isReturnDoor) 
        {
            PlaySound(true);
            ChangeScene();
        }
        else
        {
            if (PlayerInventory.PIn_Instance.checkItem(keyObject) && 
                (keyObject == PlayerController.PC_Instance.itemDataOfItemInHand))
            {
                PlaySound(true);
                PlayerInventory.PIn_Instance.RemoveItem(PlayerController.PC_Instance.itemDataOfItemInHand,true) ;
                ChangeScene();
            }
            else 
                PlaySound(false);
        }
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public Sprite GetInteractImage()
    {
        return chImage;
    }
}
