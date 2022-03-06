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
            curPB.dialogText.text = "E - говорить\nQ - завершить";
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "policeman")
        {
            if (curPB != other.transform.GetComponent<PoliceBrain>()) { return; }
            other.transform.GetComponent<PoliceBrain>().dialogText.text = "";
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.transform.tag == "policeman")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<PoliceBrain>().StartDialog();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                other.GetComponent<PoliceBrain>().EndDialog();
            }
        }
    }

}
