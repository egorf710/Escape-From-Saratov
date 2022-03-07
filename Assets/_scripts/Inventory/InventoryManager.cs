using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class InventoryManager : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public GameObject InventoryUI;
    public PlayerController playerController;
    public Crafts craftManager;
    public FightSystem fightSystem;
    void Start()
    {
        craftManager = GetComponent<Crafts>();
        slots = FindObjectsOfType<Slot>().ToList();

        foreach (var slot in slots)
        {
            slot.isEmpty = true;
            slot.inventoryManager = this;
            slot.crafts = craftManager;
        }
        craftManager.Init();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }
    }
    public bool AddItem(ItemObject itemObject)
    {
        foreach (var slot in slots)
        {
            if (itemObject.item.stacable)
            {
                if(slot.item == itemObject.item)
                {
                    slot.amount++;
                    slot.amountText.text = slot.amount.ToString();
                    Destroy(itemObject.gameObject);
                    return true;
                }
            }
            if (slot.isEmpty)
            {
                slot.SetSlot(itemObject);
                Destroy(itemObject.gameObject);
                return true;
            }
        }
        return false;
        //true - <потеряно>, false - <потеряно>
    }
    public bool AddItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (item.stacable)
            {
                if (slot.item == item)
                {
                    slot.amount++;
                    slot.amountText.text = slot.amount.ToString();
                    return true;
                }
            }
            if (slot.isEmpty)
            {
                slot.SetSlot(item);
                return true;
            }
        }
        return false;
        //true - <потеряно>, false - <потеряно>
    }
    public bool DropItem(Item item, int amount)
    {
        // <потеряно>
        foreach (var slot in slots)
        {
            if (!slot.isEmpty && slot.item == item)
            {
                slot.amount -= amount;
                // <потеряно>
                GameObject go = new GameObject();
                go.name = item.itemName;
                go.transform.position = Camera.main.GetComponent<CameraController>()._target.position;
                go.AddComponent<SpriteRenderer>().sprite = item.sprite;
                go.AddComponent<ItemObject>().item = item;
                go.GetComponent<ItemObject>().amount = amount;
                go.AddComponent<BoxCollider2D>();
                if (slot.amount <= 0)
                {
                    slot.ClearSlot();
                }
                return true;
            }
        }
        return false;
        // false - <потеряно>
    }
    public void RemoveItem(Item item)
    {
        // <потеряно>
        foreach (var slot in slots)
        {
            if (!slot.isEmpty && slot.item == item)
            {
                slot.ClearSlot();
            }
        }
    }
    public Item FindItem(string name)
    {

        // <потеряно>
        foreach (var slot in slots)
        {
            if (!slot.isEmpty && slot.item.itemName == name)
            {
                return slot.item;
            }
        }
        return null;
    }

    internal void UseItem(Slot slot)
    {
        if(slot.item.itemName == "energy drink")
        {
            playerController.energy_Current += slot.item.point;
        }
        else if(slot.item.itemName == "Soap")
        {
            playerController.lifeTime_Current += slot.item.point;
        }
        slot.amount--;
        if(slot.amount <= 0)
        {
            slot.ClearSlot();
        }
        playerController.UpdateIndicatorsUI();
    }
}
