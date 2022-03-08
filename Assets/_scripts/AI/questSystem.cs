using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questSystem : MonoBehaviour
{
    public GameLog gameLog;
    public static questSystem _questSystem;
    private void Start()
    {
        _questSystem = this;
    }
    public static bool ChekQuest(qyestItem quest)
    {
        return _questSystem._ChekQuest(quest);
    }
    public bool _ChekQuest(qyestItem qyest)
    {
        int progress = 0;
        for (int i = 0; i < qyest.action.Length; i++)
        {
            string type = qyest.action[i].Split('=')[0];
            string taget = qyest.action[i].Split('=')[1].Split(':')[0];
            int count = Convert.ToInt32(qyest.action[i].Split(':')[1]);
            int compliteCount = 0;
            if (type.Contains("getItem"))
            {
                InventoryManager inventory = FindObjectOfType<InventoryManager>();
                compliteCount += inventory.FindItem(taget);
            }
            if (type.Contains("fight"))
            {
                foreach (var actional in gameLog.fightActions)
                {
                    if (actional.Contains("fight"))
                    {
                        if (taget.Contains("all"))
                        {
                            compliteCount++;
                        }
                        else
                        {
                            if (actional.Contains(taget))
                            {
                                compliteCount++;
                            }
                        }
                    }
                }
            }
            else if (type.Contains("punch"))
            {
                foreach (var actional in gameLog.fightActions)
                {
                    if (actional.Contains("punch"))
                    {
                        if (taget.Contains("all"))
                        {
                            compliteCount++;
                        }
                        else
                        {
                            if (actional.Contains(taget))
                            {
                                compliteCount++;
                            }
                        }
                    }
                }
            }
            else if (type.Contains("addItem"))
            {
                foreach (var actional in gameLog.itemActional)
                {
                    if (actional.Contains("addItem"))
                    {
                        if (taget.Contains("all"))
                        {
                            compliteCount++;
                        }
                        else
                        {
                            if (actional.Contains(taget))
                            {
                                compliteCount++;
                            }
                        }
                    }
                }
            }
            else if (type.Contains("dropItem"))
            {
                foreach (var actional in gameLog.itemActional)
                {
                    if (actional.Contains("dropItem"))
                    {
                        if (taget.Contains("all"))
                        {
                            compliteCount++;
                        }
                        else
                        {
                            if (actional.Contains(taget))
                            {
                                compliteCount++;
                            }
                        }
                    }
                }
            }
            else if (type.Contains("useItem"))
            {
                foreach (var actional in gameLog.itemActional)
                {
                    if (actional.Contains("useItem"))
                    {
                        if (taget.Contains("all"))
                        {
                            compliteCount++;
                        }
                        else
                        {
                            if (actional.Contains(taget))
                            {
                                compliteCount++;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var actional in gameLog.oherActinal)
                {
                    if (actional.Contains(type))
                    {
                        if (taget.Contains("all"))
                        {
                            compliteCount++;
                        }
                        else
                        {
                            if (actional.Contains(taget))
                            {
                                compliteCount++;
                            }
                        }
                    }
                }
            }
            if(compliteCount >= count)
            {
                FindObjectOfType<InventoryManager>().RemoveItem(taget, count);
                progress++;
                continue;
            }
        }
        if(progress >= qyest.action.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
