using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName= "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;


    public void AddItem(Item _item, int _amount)
    {

        if(_item.buffs.Length > 0)
        {
            SetEmptySlot(_item,_amount);
            return;
        }
       
        for(int i = 0; i < Container.Items.Length;i++)
        {
            if(Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        } 

        SetEmptySlot(_item,_amount);
    }

    public void AddItemEquip(Item _item, int _amount)
    {
       for(int i = 0; i < Container.Items.Length;i++)
        {
            if(Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }  
    }

    public bool CheckItemEquip(Item _item, int _amount)
    {
       for(int i = 0; i < Container.Items.Length;i++)
        {
            if(Container.Items[i].ID == _item.Id)
               
                return true;
            
        } 
        return false;  
    }

    public InventorySlot SetEmptySlot(Item _item,int _amount)
    {
        for(int i=0 ; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }


    public void MoveItem(InventorySlot item1,InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID,item2.item,item2.amount);
        item2.UpdateSlot(item1.ID,item1.item,item1.amount);
        item1.UpdateSlot(temp.ID,temp.item,temp.amount);
    }

    public void RemoveItem(Item _item)
    {
       for(int i = 0; i< Container.Items.Length;i++)
       {
            if(Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1,null,0);
            }
       } 
    }
[ContextMenu("Save")]
   public void Save()
    {
        string jsonData = JsonUtility.ToJson(Container);
        File.WriteAllText(Application.persistentDataPath + savePath, jsonData);
    }

[ContextMenu("Load")]
    public void Load()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + savePath);
        JsonUtility.FromJsonOverwrite(jsonData, Container);
    }

[ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
  
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[35];
    public void Clear()
    {
        for(int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(-1,new Item(),0);
        }
    }
}
[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    public UserInterface parent;
    public int ID ;
    public Item item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id,Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id,Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount +=value;
    }
    public bool CanPlaceInSlot(ItemObject _item)
    {
        if(AllowedItems.Length <= 0)
            return true;

        for(int i = 0; i < AllowedItems.Length; i++)
        {
            if(_item.type == AllowedItems[i])
                return true;
        }  

        return false;  
    }
}
