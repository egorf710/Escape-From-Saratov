using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new questItem", menuName = "Dialog/questItem")]
public class qyestItem : ScriptableObject
{
    [Header("�������� ������")]
    public string descriptional;
    [Header("��� ����� �������")]
    public string[] action; //fight=kop:5 //��������� ���� 5 ���
    [Header("��� ����� ������� � �������")]
    public Item prise;
    public int amount;
}
