using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour , IInteract
{
    

    [SerializeField] int DoorSceneIndex;
    [SerializeField] bool isReturnDoor;
    [SerializeField] GameObject keyObject;
    [SerializeField] Sprite chImage;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(DoorSceneIndex);
    }

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteraction()
    {
        if (isReturnDoor) 
        {
            ChangeScene();
        }
        else
        {
            if (PlayerInventory.PIn_Instance.checkItem(keyObject))
            {
                ChangeScene();
            }
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
