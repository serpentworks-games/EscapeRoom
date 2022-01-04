using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRaycaster : MonoBehaviour
{
    [SerializeField] float interactDistance;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, interactDistance, interactLayer))
        {
            if(hit.transform.GetComponent<Interactable>())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Hit an interactable! " + hit.transform.gameObject.name);
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(cameraPosition.position, cameraPosition.forward);
    }
}
