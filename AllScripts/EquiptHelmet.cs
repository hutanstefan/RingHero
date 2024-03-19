using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Helmet")]

public class EquipHelmet : ItemObject
{
    
    public void Awake()
    {
        type = ItemType.Helmet;
    }
}
