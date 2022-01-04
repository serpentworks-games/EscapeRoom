using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject interactUI = null;

    bool isActive = false;

    void Update()
    {
        interactUI.SetActive(isActive);
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            isActive = false;
        }
    }

}
