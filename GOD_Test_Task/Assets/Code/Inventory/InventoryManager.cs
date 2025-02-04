using System;
using System.Collections;
using System.Collections.Generic;
using Code.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    
    [SerializeField] private List<ItemSlot> itemSlots;
    
    private void Start()
    {
        openButton.onClick.AddListener(OpenInventory);
        closeButton.onClick.AddListener(CloseInventory);
    }

    private void OpenInventory()
    {
        inventory.SetActive(true);
    }

    private void CloseInventory()
    {
        inventory.SetActive(false);
    }

    public void AddItem(ItemSO item, int quantity)
{
    if (item == null || quantity <= 0)
    {
        Debug.LogWarning("Предмет или количество недействительны.");
        return;
    }
    
    if (item.Stackable)
    {
        for (int i = 0; i < itemSlots.Count && quantity > 0; i++)
        {
            if (!itemSlots[i].isFull || (itemSlots[i]._ItemSO != null && itemSlots[i]._ItemSO.ID == item.ID))
            {
                int remainingSpace = item.StackSize - itemSlots[i].Quantity;

                if (remainingSpace > 0)
                {
                    int amountToAdd = Mathf.Min(remainingSpace, quantity);
                    itemSlots[i].AddItem(item, amountToAdd);
                    quantity -= amountToAdd;
                    
                    if (!itemSlots[i].isFull)
                    {
                        itemSlots[i].isFull = true;
                    }
                }
            }
        }
        
        for (int i = 0; i < itemSlots.Count && quantity > 0; i++)
        {
            if (!itemSlots[i].isFull)
            {
                int amountToAdd = Mathf.Min(item.StackSize, quantity);
                itemSlots[i].AddItem(item, amountToAdd);
                itemSlots[i].isFull = true;
                quantity -= amountToAdd;
            }
        }
    }
    else
    {
        for (int i = 0; i < itemSlots.Count && quantity > 0; i++)
        {
            if (!itemSlots[i].isFull)
            {
                itemSlots[i].AddItem(item, 1);
                itemSlots[i].isFull = true;
                quantity--;
            }
        }
    }
    
    if (quantity > 0)
    {
        Debug.LogError($"Не хватает слотов для добавления {quantity} предметов \"{item.Name}\".");
    }
}
}
