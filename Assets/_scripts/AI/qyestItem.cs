using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new questItem", menuName = "Game/Dialog/questItem")]
public class qyestItem : ScriptableObject
{
    [Header("�������� ������"), TextArea(1, 100)]
    public string descriptional;
    [Header("��� ����� �������")]
    public string[] action; //fight=kop:5 //��������� ���� 5 ���
    [Header("��� ����� ������� � �������")]
    public Item prise;
    public int amount;
}
