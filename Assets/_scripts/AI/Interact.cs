using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public PoliceBrain curPB;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "policeman")
        {
            if(curPB != null) { return; }
            curPB = other.transform.GetComponent<PoliceBrain>();
            curPB.dialogText.text = "E - Ќј„ј“№/хвалить\nR - дразнить\nQ - завершить";
            curPB.dialogUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "policeman")
        {
            if (curPB != other.transform.GetComponent<PoliceBrain>()) { return; }
            curPB.dialogText.text = "";
            curPB.EndDialog();
            curPB = null;
        }
    }
    private void Update()
    {
        if(curPB == null) { return; }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!curPB.inDialog)
            {
                curPB.StartDialog();
            }
            else
            {
                curPB.Pohvala();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            curPB.Draznit();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            curPB.EndDialog();
        }
    }
}
