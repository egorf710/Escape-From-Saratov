using UnityEngine;

[CreateAssetMenu(fileName = "Page_Item_Information", menuName = "Game/Info/New Page Item Information")]
public class PageItemInformation : ScriptableObject
{
    [Tooltip("�������� ��������")] public string _name;
    [Tooltip("������ ��������")] public Sprite _sprite;
    [Tooltip("�������� ��������"), TextArea(0, 100)] public string _description;
    [Tooltip("��������� �� �������?")] public bool isCrafting;
    [Tooltip("�������� ��� ������ (1 ������� � ������� = 1 �������)")] public string[] itemsForCraft;
    //[Tooltip("����� ��������")] public int pageNum;
}
