using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrawerOpenClose : MonoBehaviour
{

    public List<Drawer> interacts = new List<Drawer>();


    private float elapsedTime = 0f;
    private float targetTime = .1f;
    private bool canAnimate = false;
    // Update is called once per frame
    void Update()
    {
        if (canAnimate)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= targetTime)
            {
                interacts[Random.Range(0, interacts.Count - 1)].OnInteraction();
                elapsedTime = 0.0f;
            }
        }
    }

    public void ChangeAnimState()
    {
       canAnimate = !canAnimate;
    }
}
