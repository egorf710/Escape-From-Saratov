using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceBrain : MonoBehaviour
{
    public DialogItem dialogItem;
    public Text dialogText;
    public bool inDialog;
    public int angry;
    private void Start()
    {
        StartCoroutine(randmFraza());
    }
    public void StartDialog()
    {
        dialogText.text = dialogItem.startFrazi[Random.Range(0, dialogItem.startFrazi.Length)];
        inDialog = true;
    }
    public void Pohvala()
    {
        angry-=5;
        UpdateText();
    }
    public void Draznit()
    {
        angry+=10;
        UpdateText();
    }
    void UpdateText()
    {
        if(angry < 10)
        {
            dialogText.text = dialogItem.normFrazi[Random.Range(0, dialogItem.normFrazi.Length)];
        }
        else if(angry >= 10 && angry <= 30)
        {
            dialogText.text = dialogItem.bullingFrazi1[Random.Range(0, dialogItem.bullingFrazi1.Length)];
        }
        else if (angry >= 30 && angry <= 50)
        {
            dialogText.text = dialogItem.bullingFrazi2[Random.Range(0, dialogItem.bullingFrazi2.Length)];
        }
        else if(angry >= 50)
        {
            dialogText.text = dialogItem.bullingFrazi3[Random.Range(0, dialogItem.bullingFrazi3.Length)];
        }
    }
    public void EndDialog()
    {
        dialogText.text = dialogItem.endFrazi[Random.Range(0, dialogItem.endFrazi.Length)];
        inDialog = false;
    }
    IEnumerator randmFraza()
    {
        while (true)
        {
            if (!inDialog)
            {
                yield return new WaitForSeconds(Random.Range(5, 10));
                if (inDialog) { yield return null; }
                dialogText.text = dialogItem.randomFrazi[Random.Range(0, dialogItem.randomFrazi.Length)];
            }
            yield return new WaitForSeconds(Random.Range(5, 10));
            dialogText.text = "";
        }
    }
}
