using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page_Story", menuName = "Game/Info/New Page Story")]
public class PageStory : ScriptableObject
{
    [Tooltip("���� ����� ��� � �������� �������"), TextArea(0, 100)] public string _descrition;
    //[Tooltip("����� ��������")] public int pageNum;
}
