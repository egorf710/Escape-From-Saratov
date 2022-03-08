using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new DialogItem", menuName = "Game/Dialog/DialogItem")]
public class DialogItem : ScriptableObject
{
    public string[] startFrazi;
    public string[] endFrazi;
    public string[] randomFrazi;
    public string[] bullingFrazi1;
    public string[] bullingFrazi2;
    public string[] bullingFrazi3;
    public string[] normFrazi;
}
