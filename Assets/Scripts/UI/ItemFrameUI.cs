using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrameUI : MonoBehaviour
{
    public ItemData id;
    [SerializeField] Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        if (id != null)
        {
            itemImage.sprite = id.icon;
            itemImage.color = Color.white;
        }
    }

    public void updateFrameImage(ItemData itemData)
    {
        id = itemData;
        if (itemData == null)
        { 
            itemImage.enabled = false;
            return;
        }
        itemImage.enabled = true;
        itemImage.sprite = id.icon;
        itemImage.color = Color.white;

    }
}
