using System;
using System.Collections.Generic;
using Code.Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Saves
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] private Button restartGameButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private InventoryStorageService inventoryStorageService;
        private void Start()
        {
            restartGameButton.onClick.AddListener(Restart);
            nextLevelButton.onClick.AddListener(NextLevel);
        }

        private void Restart()
        {
            Debug.Log("Restarting game");

            var list = new List<ItemData>();
            list.Clear();
            inventoryStorageService.SaveInventory(list);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void NextLevel()
        {
            Debug.Log("next level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}