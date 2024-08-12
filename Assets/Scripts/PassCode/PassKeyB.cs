using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassKeyB : MonoBehaviour
{

    [SerializeField] char value;
    [SerializeField] PassCode_Mechanics pcm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        pcm.CodeButtonInteract(value);
    }

    // Update is called once per frame
    public char getValue()
    {
        return value;
    }
}
