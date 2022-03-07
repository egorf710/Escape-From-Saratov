using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("название")]
    public string itemName;
    [Header("Описание")]
    public string description = "Предмет";
    [Header("спрайт")]
    public Sprite sprite;
    [Header("сколько добавляет при использовании, енергия, мыло, манаи т.д")]
    public int point;
    [Header("стакается или нет")]
    public bool stacable;
    [Header("Рецепт для крафта, примеры в коментах скрипта")]
    //[Header("Soap+Rope=damage:20")]
    //[Header("Soap+Rope=damage")] Не обязательно что то передававать если твой евент это не использует
    //[Header("Название предмета1+Название предмета2=имя метода в скрипте Events")] Не обязательно что то передававать если твой евент это не использует
    //[Header("Soap+Rope=addItem:enegry drink")]
    //[Header("Soap+Rope=event:death (deat это название метода в скрипте Events, который выполнится через 1f)")]
    public string[] recipes;
    [Header("Насколько буллинговый")]
    [Range(0,3)]
    public int bulling = 0;
}
