using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADDLOG : MonoBehaviour
{
    public string action;
    public void AddLog()
    {
        GameLog.AddAction(action);
    }
}
