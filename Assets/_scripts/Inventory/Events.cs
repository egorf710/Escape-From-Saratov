using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft;
public class Events : MonoBehaviour
{
    //Соблюдаем читаемость кода, каждый евент подписываем и т.д
    [Header("Event1 Damaged")]
    //damage
    [SerializeField] PlayerController playerController;

    [Header("Event2")]
    //addItems
    [SerializeField] InventoryManager inventoryManager; 
    [Header("Event3")]
    //niggers
    [Header("Event4")]
    //...

    [Header("Components")]
    public static Events eventsMager; // это нужно что бы работала ХЕРНЯ ВЫШЕ
    public dynamic localPoint; //сюда временно записывается урон, лечение, состояние и т.д то что используется во время ивента
    private void Awake()
    {
        eventsMager = this;
        //ХЕРНЯ ВЫШЕ:
        //нужно для того что бы можно было вызвать ивент из любого скрипта не инициализируя скрипт Events
        //То есть не надо писать publick Events ev; ev.StartEvent("название");
        //Можно просто написать Events.StartEvent("название");
    }
    public static void StartEvent(string eventName, dynamic arguments)
    {
        eventsMager._StartEvent(eventName, arguments); // это нужно что бы работала ХЕРНЯ ВЫШЕ
    }
    private void _StartEvent(string eventName, dynamic arguments)
    {
        localPoint = arguments;
        Invoke(eventName, 1f); //сюда передаётся название метода и время через которое он выполнится "Event1_DamagePlayer", 1f(по стандарту)
    }

    //EVENT 1
    private void event_damage()
    {
        playerController.lifeTime_Current -= (float)localPoint; //это не обязательно но лучше приводить dynamic к float т.к lifeTime_Current тоже float
        playerController.UpdateIndicatorsUI();
    }

    //Для создания евента просто создай метод который будет делать что так и в настройке предета (скриптбл обджект)
    //укажи в recipe новый крафт с использованием этого евента типо Soap+Rope=event_damage:20  20 это передаваемый урон,
    //Другой пример BlackSoap+Human=event_test  тут не обязательно передавать аргумент т.к в евенте он не используется
    
    //EVENT2
    private void addItem()
    {
        //в этом случае localPoint это название предмета который получится после крафта
        Item resultItem = (Item)Resources.Load("Items/" + localPoint);
        if (!inventoryManager.FindEmpySlot())
        {
            GameObject go = new GameObject();
            go.name = resultItem.itemName;
            go.transform.position = Camera.main.GetComponent<CameraController>()._target.position;
            go.AddComponent<SpriteRenderer>().sprite = resultItem.sprite;
            go.AddComponent<ItemObject>().item = resultItem;
            go.GetComponent<ItemObject>().amount = 1;
            go.AddComponent<BoxCollider2D>();
        }
        else
        {
            inventoryManager.AddItem(resultItem);
        }
    }

    //EVENT3
    private void event_test()
    {
        print("niggers");
    }
}
