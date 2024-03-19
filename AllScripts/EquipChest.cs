using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Chestplate")]

public class EquipChest : ItemObject
{
    
    public void Awake()
    {
        type = ItemType.Chest;
    }
}
