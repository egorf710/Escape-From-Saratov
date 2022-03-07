using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEvent : MonoBehaviour
{
    public void DiePlayer()
    {
        FindObjectOfType<PlayerController>().GameOver();
        FindObjectOfType<FightSystem>().EndDialog();
    }
    public void DieEnemy()
    {
        FindObjectOfType<FightSystem>().EndDialog();
        FindObjectOfType<FightSystem>().DropItem();
    }
}
