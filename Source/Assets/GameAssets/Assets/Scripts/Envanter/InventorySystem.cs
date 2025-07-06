// using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }
    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;
    
    public bool isOpen;

    // public bool isFull;  // Belki kullanýlýr

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); Debug.Log("Inventory System Tarafýndan Destroy edildi! " + gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Editörden ne seçiliyse ona göre kapatýp açýcak
        inventoryScreenUI.SetActive(isOpen);
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);

            }
        }
    }


    void Update()
    {
        // O tuþu - Sadece envanter aç/kapa
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleInventory();
        }
        // Fare tekerleði - Sadece mouse kilidi aç/kapa
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            ToggleMouseLock();
        }
        // I tuþu - Her ikisini de yap
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
            ToggleMouseLock();
        }


    }


    public void AddToInventory(string ItemName)
    {
        whatSlotToEquip = FindNextEmptySlot();

        // Boþ slot bulunamadýysa null check yap
        if (whatSlotToEquip == null || whatSlotToEquip.name == "")
        {
            Debug.LogError("Boþ slot bulunamadý!");
            return;
        }

        itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(ItemName);
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null; // new GameObject() yerine null döndür
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount>0)
            {
                counter += 1;
            }
        }

        if (counter == 9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryScreenUI.SetActive(isOpen);
    }

    private void ToggleMouseLock()
    {
        MouseMovement.Instance.SetMouseLock(!MouseMovement.Instance.isMouseLocked);
    }

    public void RemoveFromInventory(string itemName)
    {
        // itemList'ten kaldýr
        for (int i = itemList.Count - 1; i >= 0; i--)
        {
            if (itemList[i] == itemName)
            {
                itemList.RemoveAt(i);
                break; // Sadece ilk bulduðunu sil
            }
        }

        Debug.Log($"{itemName} envanterden silindi. Kalan item sayýsý: {itemList.Count}");
    }

}