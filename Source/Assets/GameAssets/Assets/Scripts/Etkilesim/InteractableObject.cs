using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionDistance = 3f;
    [Tooltip("Eðer envantere alýnacaksa Resources klasöründe ayný isimde olmalý")]
    public string ItemName = "YAZILACAK";
    [Tooltip("Görüntülenecek etkileþim mesajý")]
    public string interactionMessage = "YAZILACAK";
    public bool sadeceBilgi = false; // True olursa sadece info ve yazý
    public bool envantereAL = false; // True olursa envantere alýnýr
    public bool agaciKes = false;    // True olursa agac kesilir

    public string GetItemName() => ItemName;
    public string GetInteractionMessage() => interactionMessage;

    private void Update()
    {
        // Eðer E tuþuna basýlýrsa (Envantere Alma Eylemi)
        if (Input.GetKeyDown(KeyCode.E) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == this)
        {
            if (envantereAL)
            {
                // Eðer envanter dolu deðil ise envantere ekle
                if (!InventorySystem.Instance.CheckIfFull())
                {
                    InventorySystem.Instance.AddToInventory(ItemName);
                    Destroy(gameObject);
                    // Debug.Log("Item envantere eklendi: " + ItemName);
                }
                else
                {
                    Debug.Log("Item envanteri dolu! " + ItemName);
                }
            }
            else if (sadeceBilgi)
            {
                // Debug.Log("Sadece bilgi seçili! " + ItemName);
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
                // Debug.Log("Sadece bilgi seçili! " + ItemName);
            }
            else
            {
                Debug.Log("Yanlýþ Tuþa Basýldý! " + ItemName);
            }
        }





    }


}