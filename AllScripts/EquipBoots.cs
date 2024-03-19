using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Boots")]

public class EquipBoots : ItemObject
{
    
    public void Awake()
    {
        type = ItemType.Boots;
    }
}
