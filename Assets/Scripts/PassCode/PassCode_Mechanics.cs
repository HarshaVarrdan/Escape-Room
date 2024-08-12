using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class PassCode_Mechanics : MonoBehaviour
{

    [SerializeField] string unlockCode;
    [SerializeField] TMP_Text codeText; 

    public void CodeButtonInteract(char val)
    {

        char[] opt = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        if(opt.Any(x => x == val)){
            addCharToText(val);
        }
        else if(val == 'c')
        {
            removeCharToText();
        }
        else if (val == 'x')
        {
            clearText();
        }
        else if(val == 's')
        {
            submitCode();
        }
    }

    void addCharToText(char val)
    {
        if(codeText.text.Length < 5)
            codeText.text += val;
    }

    void removeCharToText()
    {
        if (codeText.text.Length > 0)
        {
            codeText.text = codeText.text.Remove(codeText.text.Length - 1);
        }
    }

    void clearText()
    {
        codeText.text = "";
    }

    void submitCode()
    {
        if(codeText.text == unlockCode)
        {
            Debug.Log("Success");
        }
    }
}
