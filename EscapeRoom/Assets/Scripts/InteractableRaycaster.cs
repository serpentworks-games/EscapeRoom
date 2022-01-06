using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableRaycaster : MonoBehaviour
{
    [SerializeField] float interactDistance;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Camera cameraPosition;
    [Header("Item Pickup Variables")]
    [SerializeField] float pickupTime;
    [SerializeField] RectTransform pickupUIRoot;
    [SerializeField] Image pickupProgressRing;
    [SerializeField] TextMeshProUGUI pickupTextComponent;
    [Header("Interaction Variables")]
    [SerializeField] float interactionTime;
    [SerializeField] RectTransform interactUIRoot;
    [SerializeField] Image interactProgressRing;
    [SerializeField] TextMeshProUGUI interactTextComponent;

    Ray ray;
    float timeSincePickupStarted = Mathf.Infinity;
    float timeSinceInteractStarted = Mathf.Infinity;
    PickupItem itemToPickup;
    Interactable objectToInteractWith;

    // Update is called once per frame
    void Update()
    {
        GetInteractionRay();

        if (HasItemTargeted())
        {
            pickupUIRoot.gameObject.SetActive(true);
            {
                if (Input.GetMouseButton(0))
                {
                    PickupAction();
                }
                else
                {
                    timeSincePickupStarted = 0f;
                }
            }
            UpdateProgressRing(pickupProgressRing, timeSincePickupStarted,
             pickupTime);
        }
        else if (HasObjectToInteractWith())
        {
            interactUIRoot.gameObject.SetActive(true);
            {
                if (Input.GetMouseButton(0))
                {
                    InteractAction();
                }
                else
                {
                    timeSinceInteractStarted = 0f;
                }
            }
            UpdateProgressRing(interactProgressRing, timeSinceInteractStarted, interactionTime);
        }
        else
        {
            pickupUIRoot.gameObject.SetActive(false);
            interactUIRoot.gameObject.SetActive(false);

            timeSinceInteractStarted = 0f;
            timeSincePickupStarted = 0f;
        }
    }

    private void GetInteractionRay()
    {
        ray = cameraPosition.ViewportPointToRay(Vector3.one / 2f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if (hit.collider.GetComponent<PickupItem>() != null)
            {
                var target = hit.collider.GetComponent<PickupItem>();

                if (target == null)
                {
                    itemToPickup = null;
                    return;
                }

                else if (target != null && target != itemToPickup)
                {
                    itemToPickup = target;
                    pickupTextComponent.text = "Pick Up";
                }
            }
            else if (hit.collider.GetComponent<Interactable>() != null)
            {
                var target = hit.collider.GetComponent<Interactable>();
                if (target == null)
                {
                    objectToInteractWith = null;
                    return;
                }

                else if (target != null && target != objectToInteractWith)
                {
                    objectToInteractWith = target;
                    interactTextComponent.text = objectToInteractWith.GetInteractionText();
                }
            }
        }
        else
        {
            objectToInteractWith = null;
            itemToPickup = null;
            Debug.Log("Nothing to do");
        }
    }

    void PickupAction()
    {
        timeSincePickupStarted += Time.deltaTime;
        if (timeSincePickupStarted >= pickupTime)
        {
            itemToPickup.Pickup();
            Debug.Log("Picking up item!");
        }
    }

    void InteractAction()
    {
        timeSinceInteractStarted += Time.deltaTime;
        if (timeSinceInteractStarted >= interactionTime)
        {
            objectToInteractWith.Activate();
            Debug.Log("Interacted with the object!");
        }
    }

    void UpdateProgressRing(Image image, float progressTime, float time)
    {
        float amount = progressTime / time;
        image.fillAmount = amount;
    }

    bool HasItemTargeted()
    {
        return itemToPickup != null;
    }

    bool HasObjectToInteractWith()
    {
        return objectToInteractWith != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(ray.origin, ray.direction * 2f);
    }
}
