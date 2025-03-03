using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code;
using Code.Inventory;
using Code.Saves;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    
    [SerializeField] private List<ItemSlot> itemSlots;
    
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    
    [SerializeField] private Button deleteButton;
    [SerializeField] private InventoryStorageService inventoryStorageService;
    
    private Popup popup;
    private List<ItemData> itemDatas = new List<ItemData>();
    
    public static Action<List<ItemData>> OnInventoryLoaded;

    private void OnEnable()
    {
        OnInventoryLoaded += InitInventory;
    }

    private void OnDisable()
    {
        OnInventoryLoaded -= InitInventory;
    }

    private void Start()
    {
        inventoryStorageService.LoadInventory();
        
        popup = transform.GetComponent<Popup>();
        
        openButton.onClick.AddListener(OpenInventory);
        closeButton.onClick.AddListener(CloseInventory);
        deleteButton.onClick.AddListener(DeleteSelectSlot);
    }

    private void InitInventory(List<ItemData> Items)
    {
        Debug.Log("InitInventory");

        if (Items == null)
        {
            Items = new List<ItemData>();
            return;
        }
        
        itemDatas = Items;
        for (int i = 0; i < Items.Count; i++)
        {
            itemSlots[i].AddItem(itemDatas[i].Item, itemDatas[i].Quantity);
            
        }
    }

    private void OpenInventory()
    {
        inventory.SetActive(true);
        popup.Show();
    }

    private void CloseInventory()
    {
        inventory.SetActive(false);
        popup.Hide();
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

        Save();
    }

    public void DeselectAll()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].Deselect();
        }
    }

    public void ViewSlot(ItemSO item, int quantity)
    {
        itemImage.sprite = ItemIcons.instance.icons[item.ID];
        itemName.text = item.Name;
        itemDescription.text = item.Description;

        if (quantity == 1)
        {
            itemQuantity.text = "";
        }
        else
        {
            itemQuantity.text = quantity.ToString();
        }
        
    }
    
    public void ViewSlot()
    {
        itemImage.sprite = null;
        itemName.text = "";
        itemDescription.text = "";
        itemQuantity.text = "";
    }

    public void DeleteSelectSlot()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].isSelected)
            {
                itemSlots[i].Clear();
            }
        }
        
        Save();
    }

    public List<ItemSlot> GetItemSlots(int id)
    {
        return itemSlots.FindAll(x => x._ItemSO.ID == id).ToList();
    }
    
    public List<ItemSlot> GetItemSlots()
    {
        return itemSlots;
    }

    public void Save()
    {
        itemDatas.Clear();
        for (int i = 0; i < itemSlots.Count; i++)
        {
            var newData = new ItemData();
            newData.Item = itemSlots[i]._ItemSO;
            newData.Quantity = itemSlots[i].Quantity;
            itemDatas.Add(newData);
        }
        
        
        
        inventoryStorageService.SaveInventory(itemDatas);
    }
}
