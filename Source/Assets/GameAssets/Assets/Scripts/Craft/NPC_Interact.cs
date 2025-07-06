using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interact : MonoBehaviour
{
    public float interactionDistance = 3f;
    public string NPC_Ismi;
    public bool Demirci_Takas = false;
    public bool Üretim_Tezgahý = false;
    public bool NPC3 = false;

    // UI Referanslarý
    public GameObject takasUI;     // Inspector'dan atanacak
    public GameObject tezgahUI;    // Inspector'dan atanacak

    public string Get_NPC_Ismi()
    {
        return NPC_Ismi;
    }

    private void Start()
    {
        // UI'larý baþlangýçta kapat
        if (takasUI != null) takasUI.SetActive(false);
        if (tezgahUI != null) tezgahUI.SetActive(false);
    }

    private void Update()
    {
        // E tuþuna basýlýrsa
        if (Input.GetKeyDown(KeyCode.E) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedNPC == this)
        {
            if (Demirci_Takas)
            {
                // Fare kilidini aç
                MouseMovement.Instance.SetMouseLock(false);

                if (takasUI != null)
                {
                    // Demirci UI'ýný aç
                    takasUI.SetActive(true);
                    // CraftingManager'a bildir
                    CraftingManager.Instance.OnCraftingUIOpened(CraftingType.Demirci);
                }

                Debug.Log(NPC_Ismi + " ile Etkileþim - Demirci_Takas");
            }
            else if (Üretim_Tezgahý)
            {
                // Fare kilidini aç
                MouseMovement.Instance.SetMouseLock(false);

                if (tezgahUI != null)
                {
                    // Craft UI'ýný aç
                    tezgahUI.SetActive(true);
                    // CraftingManager'a bildir
                    CraftingManager.Instance.OnCraftingUIOpened(CraftingType.UretimTezgahi);
                }

                Debug.Log(NPC_Ismi + " ile Etkileþim - Üretim_Tezgahý");
            }
            else if (NPC3)
            {
                Debug.Log(NPC_Ismi + " ile Etkileþim - NPC3");
            }
        }

        // Q tuþu ile açýk olan UI'larý kapatma
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (takasUI != null && takasUI.activeSelf)
            {
                CloseTakasUI();
            }
            else if (tezgahUI != null && tezgahUI.activeSelf)
            {
                CloseTezgahUI();
            }
        }


    }

    // UI'larý kapatmak için yardýmcý metodlar
    public void CloseTakasUI()
    {
        if (takasUI != null)
        {
            takasUI.SetActive(false);
            MouseMovement.Instance.SetMouseLock(true);
        }
    }

    public void CloseTezgahUI()
    {
        if (tezgahUI != null)
        {
            tezgahUI.SetActive(false);
            MouseMovement.Instance.SetMouseLock(true);
        }
    }
}