using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Вспомогательные компоненты")]
    public Image iconSlot;
    public Text amountText;
    [Header("Хранящийся предмет")]
    public int amount;
    public Item item;
    private Vector2 lastMousePosition;
    [Header("Занят или нет")]
    public bool isEmpty; //true - пустой
    public void SetSlot(ItemObject itemObject)
    {
        // Настраиваем слот
        item = itemObject.item;
        amount = itemObject.amount;
        iconSlot.sprite = itemObject.item.sprite;
        isEmpty = false;
        iconSlot.color = new Color(1, 1, 1, 1);// не прозрачный
        amountText.text = amount.ToString();
    }
    public void Drop()
    {
        if(item == null) { return; }
        // Могу позволить сделать так ибо дроп вызывается редко
        GameObject go = new GameObject();
        go.name = item.itemName;
        go.transform.position = Camera.main.GetComponent<CameraController>()._target.position;
        go.AddComponent<SpriteRenderer>().sprite = item.sprite;
        go.AddComponent<ItemObject>().item = item;
        go.GetComponent<ItemObject>().amount = amount;
        go.AddComponent<BoxCollider2D>().isTrigger = true;

        amount--;
        amountText.text = amount.ToString();
        if (amount <= 0) { ClearSlot(); }
    }
    public void ClearSlot()
    {
        // Очищаемся слот
        iconSlot.color = new Color(1, 1, 1, 0); // делаем прозрачную иконку
        item = null;
        amount = 0;
        isEmpty = true;
        amountText.text = "";
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) { return; }
        lastMousePosition = eventData.position; // переходим в место касания
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item == null) { return; }

        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = iconSlot.GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);

        rect.position = newPosition; // двигаемся за курсором
        lastMousePosition = currentMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item == null) { return; }
        iconSlot.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position; // возвращаемся в исходное положение
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null) { return; }
    }
}
