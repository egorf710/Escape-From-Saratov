using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Название предмета, для проверки на наличие и т.д")]
    public string itemName;
    [Header("Иконка предмета")]
    public Sprite sprite;
    [Header("Если это мыло: сколько энергии даёт за использование")]
    public int point;
    [Header("Стакается или нет")]
    public bool stacable;
}
