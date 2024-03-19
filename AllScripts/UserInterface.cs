using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.ComponentModel;
using Unity.VisualScripting;


public abstract class UserInterface : MonoBehaviour
{

    public GrabItem player;
    public InventoryObject inventory;
  
    public Dictionary<GameObject,InventorySlot> itemsDisplayed = new Dictionary<GameObject,InventorySlot>();
    
    void Start()
    {
        for(int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }

        CreateSlots();
    }

    void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach(KeyValuePair<GameObject,InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.ID >= 0 && _slot.Value.amount > 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
 

    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData>action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }


    public void OnEnter(GameObject obj)
    {
        player.mouseItem.hoverObj = obj;
        if(itemsDisplayed.ContainsKey(obj))
            player.mouseItem.hoverItem = itemsDisplayed[obj];

    }
    public void OnExit(GameObject obj)
    {
        player.mouseItem.hoverObj = null;
        player.mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50,50);
        mouseObject.transform.SetParent(transform.parent);
        if(itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        } 

        player.mouseItem.obj = mouseObject;
       player.mouseItem.item = itemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj)
    {

        var itemOnMouse = player.mouseItem;
        var mouseHoverItem = itemOnMouse.hoverItem;
        var mouseHoverObj = itemOnMouse.hoverObj;
        var GetItemObject = inventory.database.GetItem;

        


        if(mouseHoverObj)
        {
            Debug.Log(mouseHoverItem.parent);
            if(mouseHoverItem.CanPlaceInSlot(GetItemObject[itemsDisplayed[obj].ID]) && (mouseHoverItem.item.Id <= -1 || itemsDisplayed[obj].CanPlaceInSlot(GetItemObject[mouseHoverItem.item.Id])))
            inventory.MoveItem(itemsDisplayed[obj], mouseHoverItem.parent.itemsDisplayed[itemOnMouse.hoverObj]);
        }
        else
        {
            //inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(itemOnMouse.obj);
        itemOnMouse.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if(player.mouseItem.obj != null)
            player.mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    
}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
