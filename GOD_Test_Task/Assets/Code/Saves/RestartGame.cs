using System;
using System.Collections;
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


            StartCoroutine(ClearInventoryDelay());
        }

        private void NextLevel()
        {
            Debug.Log("next level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator ClearInventoryDelay()
        {
            inventoryStorageService.SaveInventory(new List<ItemData>());
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}