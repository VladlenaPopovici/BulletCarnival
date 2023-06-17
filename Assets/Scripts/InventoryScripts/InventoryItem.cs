using System;

namespace InventoryScripts
{
    [Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        public int stackSize;

        public InventoryItem(ItemData item, int count)
        {
            itemData = item;
            AddToStack(count);
        }

        public void AddToStack(int count)
        {
            stackSize += count;
        }

        public void RemoveFromStack(int count)
        {
            stackSize -= count;
        }

    }
}
