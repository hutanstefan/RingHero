using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.ComponentModel;
using Unity.VisualScripting;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;
    public PlayerStats playerStats;
    
    public ItemDatabaseObject database;
    public Quest quest;

    public GameObject playerSword;



    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = slots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);

        }
    }

    bool swordFound;

    void Update()
    {
        UpdateSlots();
        swordFound = false;
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.item.Id >= 0)
            {
                if (inventory.database.GetItem[_slot.Value.item.Id].type == ItemType.Sword)
                {
                    swordFound = true;
                    playerSword.SetActive(true);
                }
            }
        }
        if (!swordFound)
        {
            playerSword.SetActive(false);
        }
    }

    public void UseItem()
    {

        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {

            if (_slot.Value.item.Id >= 0)
            {
                if (inventory.database.GetItem[_slot.Value.item.Id].type == ItemType.Use)
                {
                    if (_slot.Value.amount >= 0) _slot.Value.amount--;

                    if (_slot.Value.amount >= 1)
                    {
                       
                        playerStats.HP = playerStats.MAX_HP;     

                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                        _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount.ToString("n0");
                    }
                    else if (_slot.Value.amount == 0)
                    {

                        playerStats.HP = playerStats.MAX_HP;

                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                        _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";

                    }
                    else
                    {
                        _slot.Value.amount = 0;

                        Debug.Log(playerStats.HP);

                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                        _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                }
            }
        }
    }

    public void QuestBlackSmith()
    {
        Debug.Log("Do quest");

        bool sword = false;
        bool helmet = false;
        bool chest = false;
        bool leg = false;
        bool boots = false;

        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {

            if (_slot.Value.item.Id >= 0)
            {

                if (inventory.database.GetItem[_slot.Value.item.Id].type == ItemType.Sword) sword = true;

                if (inventory.database.GetItem[_slot.Value.item.Id].type == ItemType.Helmet) helmet = true;

                if (inventory.database.GetItem[_slot.Value.item.Id].type == ItemType.Chest) chest = true;

                if (inventory.database.GetItem[_slot.Value.item.Id].type == ItemType.Leg) leg = true;

                if (inventory.database.GetItem[_slot.Value.item.Id].type == ItemType.Boots) boots = true;
            }

        }

        if (sword && helmet && chest && leg && boots)
        {
            quest.questBlacksmithCompleted = true;
            quest.questBlacksmithActive = false;
            quest.questCompleted++;
        }

    }
}
