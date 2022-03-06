using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new DialogItem", menuName = "Dialog/DialogItem")]
public class DialogItem : ScriptableObject
{
    public string[] startFrazi;
    public string[] endFrazi;
    public string[] randomFrazi;
    public string[] bullingFrazi;
}
