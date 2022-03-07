using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    public static Log logManager;
    public Text text;
    public GameObject logPanel;
    private void Start()
    {
        logManager = this;
    }
    public static void toLog(string msg)
    {
        logManager.log(msg);
    }

    private void log(string msg)
    {
        logPanel.SetActive(true);
        text.text = msg;
    }
}
