using UnityEngine;

[CreateAssetMenu(fileName = "Page_Item_Information", menuName = "Game/Info/New Page Item Information")]
public class PageItemInformation : ScriptableObject
{
    [Tooltip("Название предмета")] public string _name;
    [Tooltip("Спрайт предмета")] public Sprite _sprite;
    [Tooltip("Описание предмета"), TextArea(0, 100)] public string _description;
    [Tooltip("Крафтится ли предмет?")] public bool isCrafting;
    [Tooltip("Предметы для крафта (1 элемент в массиве = 1 предмет)")] public string[] itemsForCraft;
    //[Tooltip("Номер страницы")] public int pageNum;
}
