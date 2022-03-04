using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("<потеряно>")]
    public string itemName;
    [Header("<потеряно>")]
    public Sprite sprite;
    [Header("<потеряно>")]
    public int point;
    [Header("<потеряно>")]
    public bool stacable;
}
