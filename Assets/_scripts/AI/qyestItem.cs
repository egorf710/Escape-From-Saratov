using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new questItem", menuName = "Game/Dialog/questItem")]
public class qyestItem : ScriptableObject
{
    [Header("Описание квеста"), TextArea(1, 100)]
    public string descriptional;
    [Header("что нужно сделать")]
    public string[] action; //fight=kop:5 //отпиздить копа 5 раз
    [Header("Что игрок получит и сколько")]
    public Item prise;
    public int amount;
}
