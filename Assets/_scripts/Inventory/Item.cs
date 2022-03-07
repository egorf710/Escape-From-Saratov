using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("��������")]
    public string itemName;
    [Header("��������")]
    public string description = "�������";
    [Header("������")]
    public Sprite sprite;
    [Header("������� ��������� ��� �������������, �������, ����, ����� �.�")]
    public int point;
    [Header("��������� ��� ���")]
    public bool stacable;
    [Header("������ ��� ������, ������� � �������� �������")]
    //[Header("Soap+Rope=damage:20")]
    //[Header("Soap+Rope=damage")] �� ����������� ��� �� ������������ ���� ���� ����� ��� �� ����������
    //[Header("�������� ��������1+�������� ��������2=��� ������ � ������� Events")] �� ����������� ��� �� ������������ ���� ���� ����� ��� �� ����������
    //[Header("Soap+Rope=addItem:enegry drink")]
    //[Header("Soap+Rope=event:death (deat ��� �������� ������ � ������� Events, ������� ���������� ����� 1f)")]
    public string[] recipes;
    [Header("��������� �����������")]
    [Range(0,3)]
    public int bulling = 0;
}
