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
    public bool isFull;

    public bool isOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); Debug.Log("Inventory System Tarafýndan Destroy! " + gameObject);
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
        if (CheckIfFull())
        {
            Debug.Log("Envanteri dolu item alýnamadý!");
        }
        else
        {
            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(ItemName);

            
        }

    }


    private GameObject FindNextEmptySlot()
    {



    }

    private bool CheckIfFull()
    {
        throw new NotImplementedException();
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
}