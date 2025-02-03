using UnityEngine;

namespace Code.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public bool Stackable;
        public int StackSize;
    }
}