using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Use Object", menuName = "Inventory System/Items/Use")]

public class UseObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Use;
    }
}
