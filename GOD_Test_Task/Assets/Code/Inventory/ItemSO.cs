using UnityEngine;

namespace Code.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        public int ID;
        public Sprite Icon;
        public string Name;
        public string Description;
        public bool Stackable;
        public int StackSize;
    }
}