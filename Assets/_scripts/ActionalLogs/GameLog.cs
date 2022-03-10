using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLog : MonoBehaviour
{
    //GAMELOG - сюда записываются все действия в игре, что бы отловить действия для квестов и т.д

    public List<string> oherActinal;
    public List<string> fightActions;
    public List<string> itemActional;

    public static GameLog gameLog;

    private void Start()
    {
        gameLog = this;
        InvokeRepeating("ClearActionals", 0f, 600f);
    }
    public static void AddAction(string action)
    {
        gameLog._AddActional(action);
    }

    private void _AddActional(string action)
    {
        if (action.Contains("fight") || action.Contains("punch"))
        {
            fightActions.Add(action);
            return;
        }
        if (action.Contains("addItem") || action.Contains("dropItem") || action.Contains("craftItem") || action.Contains("useItem"))
        {
            itemActional.Add(action);
            return;
        }
        oherActinal.Add(action);
    }
    void ClearActionals()
    {
        oherActinal.Clear();
        fightActions.Clear();
        itemActional.Clear();
    }
}
