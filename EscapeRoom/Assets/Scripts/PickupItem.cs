using UnityEngine;

public class PickupItem : MonoBehaviour
{
    Transform pickupDestination;

    bool canPickup;
    bool pickedUp;
    Rigidbody rb;

    private void Awake()
    {
        pickupDestination = GameObject.Find("ItemHoldPosition").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!canPickup) return;

        if (canPickup && !pickedUp)
        {
            PickUpItem();
        }
        if (pickedUp && Input.GetMouseButtonUp(0))
        {
            DropItem();
        }
    }

    public void Pickup()
    {
        canPickup = true;
    }

    private void PickUpItem()
    {
        rb.useGravity = false;
        pickedUp = true;
        this.transform.position = pickupDestination.position;
        this.transform.parent = pickupDestination;
        this.GetComponent<Collider>().enabled = false;

    }

    private void DropItem()
    {
        rb.useGravity = true;
        this.transform.parent = null;
        this.GetComponent<Collider>().enabled = true;
        canPickup = false;
        pickedUp = false;
    }


}