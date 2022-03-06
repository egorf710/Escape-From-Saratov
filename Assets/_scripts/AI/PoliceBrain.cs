using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceBrain : MonoBehaviour
{
    public DialogItem dialogItem;
    public Text dialogText;
    public bool inDialog;
    private void Start()
    {
        StartCoroutine(randmFraza());
    }
    public void StartDialog()
    {
        dialogText.text = dialogItem.startFrazi[Random.Range(0, dialogItem.startFrazi.Length)];
        inDialog = true;
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
            yield return new WaitForSeconds(Random.Range(5, 10));
            if (inDialog) { yield return null; }
            dialogText.text = dialogItem.randomFrazi[Random.Range(0, dialogItem.randomFrazi.Length)];
            yield return new WaitForSeconds(Random.Range(5, 10));
            dialogText.text = "";
        }
    }
}
