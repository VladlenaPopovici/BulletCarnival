using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    [CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun", order = 1)]
    public class Gun : ScriptableObject
    {
        public bool isSemiAuto;
        public float fireRate;
        public uint damage;
        public int maxClipSize;
    }
}