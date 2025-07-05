using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interact : MonoBehaviour
{
    public float interactionDistance = 3f;
    public string NPC_Ismi;
    public bool Demirci = false;
    public bool Çalýþma_Masasý = false;
    public bool NPC3 = false;

    // UI Referanslarý
    public GameObject demirciUI;  // Inspector'dan atanacak
    public GameObject craftUI;    // Inspector'dan atanacak

    public string Get_NPC_Ismi()
    {
        return NPC_Ismi;
    }

    private void Start()
    {
        //if (demirciUI == null)
        //{
        //    demirciUI = GameObject.Find("Takas_Menüsü_UI");
        //    if (demirciUI == null)
        //        Debug.LogWarning("Takas_Menüsü_UI bulunamadý!");
        //}

        //if (craftUI == null)
        //{
        //    craftUI = GameObject.Find("Üretim_Tezgahý_UI");
        //    if (craftUI == null)
        //        Debug.LogWarning("Üretim_Tezgahý_UI bulunamadý!");
        //}

        // UI'larý baþlangýçta kapat
        if (demirciUI != null) demirciUI.SetActive(false);
        if (craftUI != null) craftUI.SetActive(false);
    }

    private void Update()
    {
        // E tuþuna basýlýrsa
        if (Input.GetKeyDown(KeyCode.E) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedNPC == this)
        {
            if (Demirci)
            {
                Debug.Log(NPC_Ismi + " ile Etkileþim - Demirci");

                // Fare kilidini aç
                MouseMovement.Instance.SetMouseLock(false);

                // Demirci UI'ýný aç
                if (demirciUI != null)
                    demirciUI.SetActive(true);
            }
            else if (Çalýþma_Masasý)
            {
                Debug.Log(NPC_Ismi + " ile Etkileþim - Craft NPC");

                // Fare kilidini aç
                MouseMovement.Instance.SetMouseLock(false);

                // Craft UI'ýný aç
                if (craftUI != null)
                    craftUI.SetActive(true);
            }
            else if (NPC3)
            {
                Debug.Log(NPC_Ismi + " ile Etkileþim");
            }
        }
    }

    // UI'larý kapatmak için yardýmcý metodlar
    public void CloseDemirciUI()
    {
        if (demirciUI != null)
        {
            demirciUI.SetActive(false);
            MouseMovement.Instance.SetMouseLock(true);
        }
    }

    public void CloseCraftUI()
    {
        if (craftUI != null)
        {
            craftUI.SetActive(false);
            MouseMovement.Instance.SetMouseLock(true);
        }
    }
}