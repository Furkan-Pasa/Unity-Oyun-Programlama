using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionDistance = 3f;
    public string ItemName;
    public bool sadeceBilgi = false; // True olursa sadece info
    public bool envantereAL = false; // True olursa envantere alýnýr
    public bool agaciKes = false;    // True olursa agac kesilir

    public string GetItemName()
    {
        return ItemName;
    }

    private void Update()
    {
        // Eðer E tuþuna basýlýrsa (Envantere Alma Eylemi)
        if (Input.GetKeyDown(KeyCode.E) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == this)
        {
            if (envantereAL)
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
                Debug.Log("Item envantere eklendi: " + ItemName);
            }
            else if (sadeceBilgi)
            {
                Debug.Log("Sadece bilgi seçili! " + ItemName);
            }
            else
            {
                Debug.Log("Yanlýþ Tuþa Basýldý! " + ItemName);
            }
        }

        // Eðer Sol Týk Basýlýrsa (Aðaç Kesme Eylemi)
        if (Input.GetKeyDown(KeyCode.Mouse0) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == this)
        {
            if (agaciKes)
            {
                Debug.Log("Aðaç Kesildi: " + ItemName);
                Destroy(gameObject);
            }
            else if (sadeceBilgi)
            {
                Debug.Log("Sadece bilgi seçili! " + ItemName);
            }
            else
            {
                Debug.Log("Yanlýþ Tuþa Basýldý! " + ItemName);
            }
        }





    }


}