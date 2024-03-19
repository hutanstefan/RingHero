using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory;
    public InventoryObject equipment;
    
   public void OnTriggerEnter(Collider other)
   {

        var item = other.GetComponent<GroundItem>();
        if(item)
        {      
               if(equipment.CheckItemEquip(new Item (item.item),1)) equipment.AddItemEquip(new Item (item.item),1);
               else inventory.AddItem(new Item (item.item),1);
               Destroy(other.gameObject);
        }
   }

   private void OnApplicationQuit()
   {
    inventory.Container.Clear();
    equipment.Container.Clear();
   }
}
