using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] TMP_Text numText;
    
    [SerializeField] int bookNumber;

    // Start is called before the first frame update
    void Start()
    {
        numText.text = bookNumber.ToString();
    }

    public int GetBookNumber()
    {
        return bookNumber;
    }
}
