using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    //��������� ���������� ����, ������ ����� ����������� � �.�
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
    public static Events eventsMager; // ��� ����� ��� �� �������� ����� ����
    public dynamic localPoint; //���� �������� ������������ ����, �������, ��������� � �.� �� ��� ������������ �� ����� ������
    private void Start()
    {
        eventsMager = this;
        //����� ����:
        //����� ��� ���� ��� �� ����� ���� ������� ����� �� ������ ������� �� ������������� ������ Events
        //�� ���� �� ���� ������ publick Events ev; ev.StartEvent("��������");
        //����� ������ �������� Events.StartEvent("��������");
    }
    public static void StartEvent(string eventName, dynamic arguments)
    {
        eventsMager._StartEvent(eventName, arguments); // ��� ����� ��� �� �������� ����� ����
    }
    private void _StartEvent(string eventName, dynamic arguments)
    {
        localPoint = arguments;
        Invoke(eventName, 1f); //���� ��������� �������� ������ � ����� ����� ������� �� ���������� "Event1_DamagePlayer", 1f(�� ���������)
    }

    //EVENT 1
    private void event_damage()
    {
        playerController.lifeTime_Current -= (float)localPoint; //��� �� ����������� �� ����� ��������� dynamic � float �.� lifeTime_Current ���� float
        playerController.UpdateIndicatorsUI();
    }

    //��� �������� ������ ������ ������ ����� ������� ����� ������ ��� ��� � � ��������� ������� (�������� �������)
    //����� � recipe ����� ����� � �������������� ����� ������ ���� Soap+Rope=event_damage:20  20 ��� ������������ ����,
    //������ ������ BlackSoap+Human=event_test  ��� �� ����������� ���������� �������� �.� � ������ �� �� ������������
    
    //EVENT2
    private void addItem()
    {
        //� ���� ������ localPoint ��� �������� �������� ������� ��������� ����� ������
        Item resultItem = (Item)Resources.Load("Items/" + localPoint);
        inventoryManager.AddItem(resultItem);
    }

    //EVENT3
    private void event_test()
    {
        print("niggers");
    }
}
