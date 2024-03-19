using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equip Object", menuName = "Inventory System/Items/Sword")]

public class EquipSword : ItemObject
{
    public void Awake()
    {
        type = ItemType.Sword;
    }
}
