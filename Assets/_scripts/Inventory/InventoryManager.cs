using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public GameObject InventoryUI;

    void Start()
    {
        slots = FindObjectsOfType<Slot>().ToList();
    }

    void Update()
    {
        foreach (var slot in slots)
        {
            slot.isEmpty = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }
    }
    public bool AddItem(ItemObject itemObject)
    {
        foreach (var slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.SetSlot(itemObject);
                Destroy(itemObject.gameObject);
                return true;
            }
            else if (slot.item.stacable)
            {
                if (slot.item == itemObject.item)
                {
                    slot.amount++;
                    slot.amountText.text = slot.amount.ToString();
                    Destroy(itemObject.gameObject);
                    return true;
                }
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
}
