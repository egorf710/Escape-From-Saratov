using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("�������� ��������, ��� �������� �� ������� � �.�")]
    public string itemName;
    [Header("������ ��������")]
    public Sprite sprite;
    [Header("���� ��� ����: ������� ������� ��� �� �������������")]
    public int point;
    [Header("��������� ��� ���")]
    public bool stacable;
}
