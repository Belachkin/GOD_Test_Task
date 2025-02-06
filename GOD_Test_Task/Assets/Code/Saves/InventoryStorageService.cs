using System;
using System.Collections.Generic;
using Code.Inventory;
using UnityEngine;

namespace Code.Saves
{
    public class InventoryStorageService : MonoBehaviour
    {
        private const string KEY = "Inventory";
        private IStorageService storageService;

        private void Start()
        {
            storageService = new JsonToFIleStorageService();
        }

        public void SaveInventory(List<ItemData> itemDatas)
        {
            storageService.Save(KEY, itemDatas);
            Debug.Log("Inventory saved");
        }

        public void LoadInventory()
        {
            storageService.Load<List<ItemData>>(KEY, InventoryManager.OnInventoryLoaded);
        }
    }
}