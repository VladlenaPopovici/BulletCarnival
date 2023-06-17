using System;
using UnityEngine;

namespace InventoryScripts
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 2)]

    public class ItemData : ScriptableObject
    {
        public string description;
        public Sprite item;

        private bool Equals(ItemData other)
        {
            return base.Equals(other) && description == other.description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((ItemData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), description);
        }
    }
}
