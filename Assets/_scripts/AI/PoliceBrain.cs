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
    public GameObject dialogUI;
    public Image smileInDialog;
    public Sprite[] smiles;
    public FightSystem fightSystem;
    public FightItem fightItem;
    public bool kop_or_zek;
    [Header("Êâåñò")]
    public int questProgress;
    public qyestItem[] qyestItem;
    public bool questInPrgcess;
    private void Start()
    {
        fightSystem = FindObjectOfType<FightSystem>();
        StartCoroutine(randmFraza());

        smileInDialog.sprite = smiles[0];
    }
    public void StartDialog()
    {
        dialogUI.SetActive(true);
        dialogText.text = dialogItem.startFrazi[Random.Range(0, dialogItem.startFrazi.Length)];
        inDialog = true;
    }
    public void Pohvala()
    {
        if (angry >= -5)
        {
            angry -= 5;
        }
        UpdateText();
    }
    public void Draznit()
    {
        if (angry <= 50)
        {
            angry += 10;
        }
        else
        {
            EndDialog();
            fightSystem.InitFight(fightItem, kop_or_zek);
        }
        UpdateText();
    }
    void UpdateText()
    {
        if(angry <= 0)
        {
            smileInDialog.sprite = smiles[0];
            if (questInPrgcess)
            {
                if (questSystem.ChekQuest(qyestItem[questProgress]))
                {
                    FindObjectOfType<InventoryManager>().SpawnItem(qyestItem[questProgress].prise, qyestItem[questProgress].amount);
                    questProgress++;
                    questInPrgcess = false;
                    angry = 0;
                }
            }
            if (questProgress < qyestItem.Length)
            {
                Log.toLog(" -ÇÀÄÀÍÈÅ- \n" + qyestItem[questProgress].descriptional + "\n ÒÛ ÏÎËÓ×ÈØÜ: " + qyestItem[questProgress].prise.itemName + "\n Ó ÒÅÁß: 10 ìèíóò");
                questInPrgcess = true;
            }
            if (kop_or_zek)
            {
                GameLog.AddAction("speak kop");
            }
            else
            {
                GameLog.AddAction("speak zek");
            }
        }
        else if (angry > 0 && angry < 10)
        {
            smileInDialog.sprite = smiles[1];
            dialogText.text = dialogItem.normFrazi[Random.Range(0, dialogItem.normFrazi.Length)];
        }
        else if(angry >= 10 && angry <= 30)
        {
            smileInDialog.sprite = smiles[2];
            dialogText.text = dialogItem.bullingFrazi1[Random.Range(0, dialogItem.bullingFrazi1.Length)];
        }
        else if (angry >= 30 && angry <= 50)
        {
            if (kop_or_zek)
            {
                GameLog.AddAction("angry kop");
            }
            else
            {
                GameLog.AddAction("angry zek");
            }
            smileInDialog.sprite = smiles[3];
            dialogText.text = dialogItem.bullingFrazi2[Random.Range(0, dialogItem.bullingFrazi2.Length)];
        }
        else if(angry >= 50)
        {
            smileInDialog.sprite = smiles[4];
            dialogText.text = dialogItem.bullingFrazi3[Random.Range(0, dialogItem.bullingFrazi3.Length)];
        }
    }
    public void EndDialog()
    {
        dialogText.text = dialogItem.endFrazi[Random.Range(0, dialogItem.endFrazi.Length)];
        inDialog = false;
        dialogUI.SetActive(false);
    }
    IEnumerator randmFraza()
    {
        while (true)
        {
            if (!inDialog)
            {
                yield return new WaitForSeconds(Random.Range(5, 10));
                if (inDialog) { yield return null; }
                dialogUI.SetActive(true);
                dialogText.text = dialogItem.randomFrazi[Random.Range(0, dialogItem.randomFrazi.Length)];
            }
            yield return new WaitForSeconds(Random.Range(5, 10));
            if (!inDialog)
            {
                dialogUI.SetActive(false);
                dialogText.text = "";
            }
        }
    }
}
