using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Çöp kutusunun görsel efektleri için
    private Image trashImage;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.red;

    void Start()
    {
        trashImage = GetComponent<Image>();
        if (trashImage == null)
            trashImage = GetComponentInChildren<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged != null)
        {
            GameObject draggedItem = DragDrop.itemBeingDragged;

            // Item'ýn adýný al (silmeden önce)
            string itemName = "";
            if (draggedItem.name.Contains("(Clone)"))
            {
                itemName = draggedItem.name.Replace("(Clone)", "").Trim();
            }
            else
            {
                itemName = draggedItem.name;
            }

            // InventorySystem'dan item'ý kaldýr
            if (!string.IsNullOrEmpty(itemName))
            {
                InventorySystem.Instance.RemoveFromInventory(itemName);
            }

            // GameObject'i yok et
            Destroy(draggedItem);

            // Debug.Log($"{itemName} çöp kutusuna atýldý!");

            // Crafting UI'larý güncelle (eðer açýksa)
            if (CraftingManager.Instance != null)
            {
                CraftingManager.Instance.UpdateCraftingUI();
            }
        }
    }

    // Mouse üzerine geldiðinde renk deðiþtir (opsiyonel)
    public void OnPointerEnter()
    {
        if (trashImage != null)
            trashImage.color = hoverColor;
    }

    public void OnPointerExit()
    {
        if (trashImage != null)
            trashImage.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (trashImage != null)
        {
            trashImage.color = hoverColor;
            // Debug.Log("Mouse çöp kutusu üzerine geldi: Renk kýrmýzýya deðiþti");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (trashImage != null)
        {
            trashImage.color = normalColor;
            // Debug.Log("Mouse çöp kutusundan çýktý: Renk beyaza döndü");
        }
    }

}