using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Компоненты")]
    public Image iconSlot;
    public Text amountText;
    [Header("Характеристика")]
    public int amount;
    public Item item;
    private Vector2 lastMousePosition;
    [Header("Пустой или нет")]
    public bool isEmpty; //true - <потеряно>
    [HideInInspector] public InventoryManager inventoryManager;
    [HideInInspector] public Crafts crafts;
    public void SetSlot(ItemObject itemObject)
    {
        // <потеряно>
        item = itemObject.item;
        amount = itemObject.amount;
        iconSlot.sprite = itemObject.item.sprite;
        isEmpty = false;
        iconSlot.color = new Color(1, 1, 1, 1);// <потеряно>
        amountText.text = amount.ToString();
    }
    public void SetSlot(Item _item)
    {
        // <потеряно>
        item = _item;
        amount = 1;
        iconSlot.sprite = _item.sprite;
        isEmpty = false;
        iconSlot.color = new Color(1, 1, 1, 1);// <потеряно>
        amountText.text = amount.ToString();
    }
    public void SetSlot(Item item, int amount)
    {
        // <потеряно>
        this.item = item;
        this.amount = amount;
        iconSlot.sprite = item.sprite;
        isEmpty = false;
        iconSlot.color = new Color(1, 1, 1, 1);// <потеряно>
        amountText.text = amount.ToString();
    }
    public void Drop()
    {
        if (item == null) { return; }
        // <потеряно>
        GameObject go = new GameObject();
        go.name = item.itemName;
        go.transform.position = Camera.main.GetComponent<CameraController>()._target.position;
        go.AddComponent<SpriteRenderer>().sprite = item.sprite;
        go.AddComponent<ItemObject>().item = item;
        go.GetComponent<ItemObject>().amount = 1;
        go.AddComponent<BoxCollider2D>().isTrigger = true;

        amount--;
        amountText.text = amount.ToString();
        if (amount <= 0) { ClearSlot(); }
    }
    public void ClearSlot()
    {
        // <потеряно>
        iconSlot.color = new Color(1, 1, 1, 0); // <потеряно>
        item = null;
        amount = 0;
        isEmpty = true;
        amountText.text = "";
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) { return; }
        lastMousePosition = eventData.position; // <потеряно>
        iconSlot.transform.SetParent(transform.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item == null) { return; }

        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = iconSlot.GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);

        rect.position = newPosition; // <потеряно>
        lastMousePosition = currentMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item == null) { return; }
        if (eventData.pointerCurrentRaycast.gameObject)
        {
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>())
            {
                Slot curSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();
                if (curSlot == this) { return; }
                if (curSlot.isEmpty)
                {
                    curSlot.SetSlot(item, amount);
                    ClearSlot();
                }
                else
                {
                    if (curSlot.item == item && curSlot.item.stacable)
                    {
                        curSlot.amount += amount;
                        curSlot.amountText.text = curSlot.amount.ToString();
                        ClearSlot();
                    }
                    else
                    {
                        //Crafts
                        string meItem = item.itemName;//firstItem for craft
                        string otherITem = curSlot.item.itemName;//lastItem for craft
                        for (int i = 0; i < item.recipes.Length; i++)
                        {
                            if (item.recipes[i].Contains(otherITem))//если мы понимаем что 2 вещи можно совместить
                            {
                                crafts.CraftItem(item.recipes[i]);
                                curSlot.amount--;
                                amount--;
                                if (curSlot.amount <= 0) { curSlot.ClearSlot(); }
                                if (amount <= 0) { ClearSlot(); }
                                amountText.text = amount.ToString();
                                curSlot.amountText.text = curSlot.amount.ToString();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                //attach-area
                if(eventData.pointerCurrentRaycast.gameObject.name == "attach-area" && item.bulling > 0)
                {
                    if (FindObjectOfType<FightSystem>().ready)
                    {
                        FindObjectOfType<FightSystem>().youAttack(item);
                        amount--;
                        amountText.text = amount.ToString();
                        if (amount <= 0)
                        {
                            ClearSlot();
                        }
                        FindObjectOfType<FightSystem>().ready = false;
                    }
                }
            }
        }
        iconSlot.transform.SetParent(transform);
        iconSlot.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position; // <потеряно>
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null) { return; }
        if(item.point == 0 || item.bulling > 0) { return; }
        if (eventData.clickCount >= 2)
        {
            inventoryManager.UseItem(this);
        }
    }
}