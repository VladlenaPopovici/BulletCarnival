using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        // FIXME delete later
        public ItemData bulletPistol;
        public ItemData bulletRifle;
        public ItemData bulletSniper;

        public List<Image> Slots = new(6);
        public List<InventoryItem> inventory = new(6);
        private Dictionary<ItemData, InventoryItem> _itemDictionary = new();

        private void Start()
        {
            // FIXME delete this later
            Add(bulletPistol, 10);
            Add(bulletPistol, 10);
            Add(bulletPistol, 10);

            Add(bulletRifle, 10);
            Add(bulletRifle, 100);
            
            Add(bulletSniper, 12);
            
            Remove(bulletPistol);
            
            gameObject.SetActive(false); // Troom Troom отдыхает
        }

        public InventoryItem Get(ItemData itemData)
        {
            return _itemDictionary.GetValueOrDefault(itemData, new InventoryItem(itemData, 0));
        }

        public void Add(ItemData itemData)
        {
            Add(itemData, 1);
        }

        public void Add(ItemData itemData, int count)
        {
            if (_itemDictionary.TryGetValue(itemData, out var item))
            {
                item.AddToStack(count);
                UpdateSlotCount(item);
            }
            else
            {
                var newItem = new InventoryItem(itemData, count);
                AddNewItemToInventory(newItem);
                inventory.Add(newItem);
                _itemDictionary.Add(itemData, newItem);
            }
        }

        private void UpdateSlotCount(InventoryItem item)
        {
            var slotIndex = inventory.IndexOf(item);
            Slots[slotIndex].GetComponentInChildren<TextMeshProUGUI>().text = item.stackSize.ToString();
        }

        private void AddNewItemToInventory(InventoryItem newItem)
        {
            var newSlotIndex = inventory.Count;
            Slots[newSlotIndex].sprite = newItem.itemData.item;
            Slots[newSlotIndex].GetComponentInChildren<TextMeshProUGUI>().text = newItem.stackSize.ToString();
        }

        public void Set(ItemData itemData, int count)
        {
            Remove(itemData, Get(itemData).stackSize);
            Add(itemData, count);
        }

        public void Remove(ItemData itemData)
        {
            Remove(itemData, 1);
        }

        public void Remove(ItemData itemData, int count)
        {
            if (!_itemDictionary.TryGetValue(itemData, out var item)) return;

            item.RemoveFromStack(count);
            UpdateSlotCount(item);

            if (item.stackSize != 0) return;

            inventory.Remove(item);
            _itemDictionary.Remove(itemData);
            RemoveSlot(item);
        }

        private void RemoveSlot(InventoryItem item)
        {
            var slotIndex = inventory.IndexOf(item);
            
            Slots[slotIndex].sprite = null;
            Slots[slotIndex].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
}