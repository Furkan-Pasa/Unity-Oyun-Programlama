using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public bool isOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        isOpen = false;
        inventoryScreenUI.SetActive(isOpen);
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