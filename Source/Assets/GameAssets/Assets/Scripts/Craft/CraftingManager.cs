using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    // UI Referanslarý
    [Header("Demirci UI")]
    public GameObject demirciUI;
    public Button takasButton;
    public TextMeshProUGUI takasButtonText;

    [Header("Üretim Tezgahý UI")]
    public GameObject uretimUI;
    public Button baltaButton;
    public TextMeshProUGUI baltaButtonText;

    // Tarifler (Ýleride SQLite'dan çekilecek)
    private List<CraftingRecipe> allRecipes = new List<CraftingRecipe>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        InitializeRecipes();

        // Buton click eventlerini baðla
        if (takasButton != null)
            takasButton.onClick.AddListener(() => CraftItem("OdunToMetal"));

        if (baltaButton != null)
            baltaButton.onClick.AddListener(() => CraftItem("Balta"));
    }

    void InitializeRecipes()
    {
        // Demirci - Odun to Metal takasý
        CraftingRecipe odunToMetal = new CraftingRecipe();
        odunToMetal.recipeName = "OdunToMetal";
        odunToMetal.resultItem = "Metal";
        odunToMetal.resultAmount = 1;
        odunToMetal.requiredItems.Add(new CraftingRecipe.RequiredItem("Odun", 4));
        odunToMetal.recipeID = 1;
        allRecipes.Add(odunToMetal);

        // Üretim Tezgahý - Balta
        CraftingRecipe baltaRecipe = new CraftingRecipe();
        baltaRecipe.recipeName = "Balta";
        baltaRecipe.resultItem = "Balta";
        baltaRecipe.resultAmount = 1;
        baltaRecipe.requiredItems.Add(new CraftingRecipe.RequiredItem("Dal", 3));
        baltaRecipe.requiredItems.Add(new CraftingRecipe.RequiredItem("Metal", 2));
        baltaRecipe.recipeID = 2;
        allRecipes.Add(baltaRecipe);
    }

    public void CraftItem(string recipeName)
    {
        CraftingRecipe recipe = allRecipes.Find(r => r.recipeName == recipeName);
        if (recipe == null)
        {
            Debug.LogError("Tarif bulunamadý: " + recipeName);
            return;
        }

        // Envanter kontrolü
        if (!recipe.CanCraft(InventorySystem.Instance.itemList))
        {
            Debug.Log("Yetersiz malzeme!");
            return;
        }

        // Eðer sonuç item zaten envanterde varsa (Balta gibi)
        if (recipe.resultItem == "Balta" && HasItemInInventory("Balta"))
        {
            Debug.Log("Zaten bir baltan var!");
            return;
        }

        // Net item deðiþikliðini hesapla
        int requiredCount = 0;
        foreach (var required in recipe.requiredItems)
        {
            requiredCount += required.amount;
        }
        int resultCount = recipe.resultAmount;
        int currentItems = InventorySystem.Instance.itemList.Count;
        int slotCount = InventorySystem.Instance.slotList.Count;

        int finalItemCount = currentItems - requiredCount + resultCount;

        if (finalItemCount > slotCount)
        {
            Debug.Log("Envanterde yeterli yer yok!");
            return;
        }

        // Malzemeleri envanterden çýkar
        foreach (var required in recipe.requiredItems)
        {
            RemoveItemsFromInventory(required.itemName, required.amount);
        }

        // Sonuç itemini ekle
        for (int i = 0; i < recipe.resultAmount; i++)
        {
            InventorySystem.Instance.AddToInventory(recipe.resultItem);
        }

        // Debug.Log($"{recipe.resultAmount} adet {recipe.resultItem} üretildi!");

        // UI'ý güncelle
        UpdateCraftingUI();
    }

    void RemoveItemsFromInventory(string itemName, int amount)
    {
        int removed = 0;

        // Önce slotlardan görsel olarak kaldýr ve hangi slotlar boþaldý kaydet
        List<GameObject> emptiedSlots = new List<GameObject>();

        foreach (GameObject slot in InventorySystem.Instance.slotList)
        {
            if (slot.transform.childCount > 0 && removed < amount)
            {
                Transform child = slot.transform.GetChild(0);
                if (child.name.Contains(itemName))
                {
                    DestroyImmediate(child.gameObject);
                    emptiedSlots.Add(slot);
                    removed++;
                }
            }
        }

        // itemList'ten kaldýr
        removed = 0;
        for (int i = InventorySystem.Instance.itemList.Count - 1; i >= 0; i--)
        {
            if (InventorySystem.Instance.itemList[i] == itemName && removed < amount)
            {
                InventorySystem.Instance.itemList.RemoveAt(i);
                removed++;
            }
        }
    }

    bool HasItemInInventory(string itemName)
    {
        return InventorySystem.Instance.itemList.Contains(itemName);
    }

    public void UpdateCraftingUI()
    {
        // Demirci UI güncelleme
        if (demirciUI != null && demirciUI.activeSelf)
        {
            CraftingRecipe odunRecipe = allRecipes.Find(r => r.recipeName == "OdunToMetal");
            bool canCraftMetal = odunRecipe.CanCraft(InventorySystem.Instance.itemList);

            if (takasButton != null)
            {
                takasButton.interactable = canCraftMetal;
                // Buton rengini deðiþtir
                ColorBlock colors = takasButton.colors;
                colors.normalColor = canCraftMetal ? Color.white : Color.gray;
                takasButton.colors = colors;
            }
        }

        // Üretim Tezgahý UI güncelleme
        if (uretimUI != null && uretimUI.activeSelf)
        {
            CraftingRecipe baltaRecipe = allRecipes.Find(r => r.recipeName == "Balta");
            bool canCraftBalta = baltaRecipe.CanCraft(InventorySystem.Instance.itemList) && !HasItemInInventory("Balta");

            if (baltaButton != null)
            {
                baltaButton.interactable = canCraftBalta;
                // Buton rengini deðiþtir
                ColorBlock colors = baltaButton.colors;
                colors.normalColor = canCraftBalta ? Color.white : Color.gray;
                baltaButton.colors = colors;
            }
        }
    }

    // NPC UI'larý açýldýðýnda çaðrýlacak
    public void OnCraftingUIOpened(CraftingType type)
    {
        UpdateCraftingUI();
    }
}