using System;
using UnityEngine;

namespace InventoryScripts
{
    public class InputLogic : MonoBehaviour
    {
        [SerializeField] private Canvas inventory;
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.B)) return;

            var inventoryGo = inventory.gameObject;
            inventoryGo.SetActive(!inventoryGo.activeSelf);
            Time.timeScale = inventoryGo.activeSelf ? 0 : 1;
        }
    }
}