using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafts : MonoBehaviour
{
    public void Init()
    {
        //test
        //CraftItem("Soap+Rope=event_damage:20");
    }
    public dynamic CraftItem(string recipe) //������� item+item, 
    {
        //������ ��� �������� 
        // Soap+Rope=event_damage:20
        // Soap+Rope=event_addItem:enegry drink
        //������� ����� �� ���������� ��� ��� ��������, ������� ��� ��������
        string item1 = recipe.Split('+')[0]; // Soap
        string item2 = recipe.Split('+')[1].Split('=')[0]; //Rope
        string result = recipe.Split('=')[1].Split(':')[0]; // damage
        dynamic baff = 0;
        if (recipe.Contains(":"))
        {
            try
            {
                baff = Convert.ToInt32(recipe.Split('=')[1].Split(':')[1]); // 20
            }
            catch
            {
                baff = recipe.Split('=')[1].Split(':')[1]; // 20
            }
        }

        Events.StartEvent(result, baff); //event_damage, 20

        return true;
    }
}
