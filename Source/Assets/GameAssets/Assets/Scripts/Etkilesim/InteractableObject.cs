using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionDistance = 3f;
    public string ItemName;
    public bool destroyOnInteraction = true; // Sadece info göstermek için false

    public string GetItemName()
    {
        return ItemName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == this)
        {
            if (destroyOnInteraction)
            {
                Debug.Log("Item envantere eklendi");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Obje ile etkileþim: " + ItemName);
            }
        }
    }


}