using System;
using System.Collections.Generic;
using UnityEngine;
using Code;
namespace Code.Inventory
{
    public class ItemIcons : MonoBehaviour
    {
        public static ItemIcons instance;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public List<Sprite> icons = new List<Sprite>();
        public List<ItemSO> itemOjects = new List<ItemSO>();
    }
}