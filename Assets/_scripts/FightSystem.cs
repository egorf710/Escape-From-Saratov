using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSystem : MonoBehaviour
{
    [Header("Stats")]
    public int you_health = 100;
    public int you_mana = 100;
    public int enemy_health = 100; // всего 100 хп
    //фраза потребляет ману что бы небыло такого что гг без урона ушёл

    public FightItem youFrazi, enemyFrazi;

    public bool fight;
    public bool ready;
    [Header("Log")]
    public Text logText;
    [Header("Other components")]
    public Animator youAnim;
    public Animator enemyAnim;
    public Text youDialog, enemyDialog;

    public GameObject CameraMain;
    public GameObject UI1;
    public GameObject UI2;
    [Header("UI")]
    public Image youHealthUI;
    public Image youManaUI;
    public Image enemyHealthUI;
    [Header("DropItem")]
    public ItemObject prefabDropItem;
    public void InitFight()
    {
        ready = true;
        fight = true;
        transform.GetChild(0).gameObject.SetActive(true);
        CameraMain.SetActive(false);
        UI1.SetActive(false);
        UI2.SetActive(false);
        you_health = 100;
        you_mana = 100;
        enemy_health = 100;
    }
    public void EndDialog()
    {
        fight = false;
        transform.GetChild(0).gameObject.SetActive(false);
        CameraMain.SetActive(true);
        UI1.SetActive(true);
        UI2.SetActive(true);
    }
    private void Update()
    {
        if (!fight) { return; }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Log("Вы сливаетесь");
            fight = false;
            Invoke("EndDialog", 1f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Log("Вы не придумали что ответить...");
            enemyAttck();
        }
    }
    public void enemyAttck()
    {
        ready = false;
        int bullingFraza = Random.Range(0, 100);
        if (bullingFraza >= 0 && bullingFraza < 50)
        {
            you_health -= 20;
            SetFraza(enemyFrazi.bulling1[Random.Range(0, enemyFrazi.bulling1.Length)], true);
            Log("Противник что то бормочит...");
            Log("Вы едва уловили суть буллинга, вас это не сильно задело");
        }
        else if (bullingFraza >= 50 && bullingFraza < 75)
        {
            SetFraza(enemyFrazi.bulling2[Random.Range(0, enemyFrazi.bulling2.Length)], true);
            you_health -= 39;
            Log("Противник готоворит о вас...");
            Log("Вам стало обидно");
        }
        else if (bullingFraza >= 75)
        {
            SetFraza(enemyFrazi.bulling3[Random.Range(0, enemyFrazi.bulling3.Length)], true);
            you_health -= 70;
            Log("Противник чётко говорит о вашем личном...");
            Log("Вы больше всего не хотели это услышать");
        }
        enemyAnim.SetTrigger("punch");
        you_mana += 20;
        if(you_health <= 0)
        {
            youAnim.SetTrigger("die");
        }
        ready = true;
    }
    public void youAttack(Item frazaItem)
    {
        if (!ready) { return; }
        you_mana -= frazaItem.point;
        if(frazaItem.bulling == 1)
        {
            enemy_health -= 25;
            SetFraza(youFrazi.bulling1[Random.Range(0, youFrazi.bulling1.Length)], false);
            Log("Вы читаете фразу на листке с слабым буллингом...");
            Log("Похоже это фраза не сильно его задела");
        }else if(frazaItem.bulling == 2)
        {
            SetFraza(youFrazi.bulling2[Random.Range(0, youFrazi.bulling2.Length)], false);
            enemy_health -= 50;
            Log("Вы читаете фразу на листке с умеренным буллингом...");
            Log("Вы замечаете что это его задело");
        }
        else if(frazaItem.bulling == 3)
        {
            SetFraza(youFrazi.bulling3[Random.Range(0, youFrazi.bulling3.Length)], false);
            enemy_health -= 100;
            Log("Вы читаете фразу на листке с смертельным буллингом...");
            Log("Похоже не стоило это ему говорить");
        }
        youAnim.SetTrigger("punch");
        if(enemy_health <= 0)
        {
            enemyAnim.SetTrigger("die");
        }
        else
        {
            Invoke("enemyAttck", 1f);
        }
    }
    public void Log(string msg)
    {
        logText.text += "\n " + msg;
        UpdateUI();
    }
    public void SetFraza(string fraza, bool enemy)
    {
        if (!enemy)
        {
            youDialog.transform.parent.gameObject.SetActive(true);
            youDialog.text = fraza;
        }
        else
        {
            enemyDialog.transform.parent.gameObject.SetActive(true);
            enemyDialog.text = fraza;
        }
        StartCoroutine(HideDialog(enemy));
    }
    public void UpdateUI()
    {
        youHealthUI.fillAmount = (float)you_health / 100f;
        youManaUI.fillAmount = (float)you_mana / 100f;
        enemyHealthUI.fillAmount = (float)enemy_health / 100f;
    }
    IEnumerator HideDialog(bool enemy)
    {
        yield return new WaitForSeconds(5f);
        if (!enemy)
        {
            youDialog.transform.parent.gameObject.SetActive(false);
            youDialog.text = "";
        }
        else
        {
            enemyDialog.transform.parent.gameObject.SetActive(false);
            enemyDialog.text = "";
        }
    }
    public void DropItem()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Instantiate(prefabDropItem, player.position, player.rotation);
    }
}
