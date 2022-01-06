using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] bool needsLongHoldToPickUp;
    [SerializeField] string interactionText;

    public bool GetNeedsLongHoldPickup()
    {
        return needsLongHoldToPickUp;
    }

    public string GetInteractionText()
    {
        if(interactionText == "")
        {
            return "Interact";
        }
        return interactionText;
    }

    public void Activate()
    {
        Debug.Log(this.name + " has been activated!");
    }
}
