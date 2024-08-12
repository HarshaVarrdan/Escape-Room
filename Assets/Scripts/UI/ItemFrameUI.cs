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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateFrameImage(ItemData itemData)
    {
        id = itemData;
        if (itemImage != null)
            itemImage.sprite = id.icon;
            itemImage.color = Color.white;

    }
}
