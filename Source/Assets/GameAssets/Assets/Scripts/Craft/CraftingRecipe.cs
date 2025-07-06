using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftingRecipe
{
    public string recipeName;
    public string resultItem;
    public int resultAmount;

    // Gerekli malzemeler
    public List<RequiredItem> requiredItems = new List<RequiredItem>();

    [System.Serializable]
    public class RequiredItem
    {
        public string itemName;
        public int amount;

        public RequiredItem(string name, int amt)
        {
            itemName = name;
            amount = amt;
        }
    }

    // SQLite için ID
    public int recipeID;

    // Tarifi kontrol et
    public bool CanCraft(List<string> inventory)
    {
        foreach (var required in requiredItems)
        {
            int count = 0;
            foreach (string item in inventory)
            {
                if (item == required.itemName)
                    count++;
            }

            if (count < required.amount)
                return false;
        }
        return true;
    }
}

// Craft türleri için enum
public enum CraftingType
{
    Demirci,
    UretimTezgahi
}