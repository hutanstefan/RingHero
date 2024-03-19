using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Leg")]

public class EquipLeg : ItemObject
{
    
    public void Awake()
    {
        type = ItemType.Leg;
    }
}
