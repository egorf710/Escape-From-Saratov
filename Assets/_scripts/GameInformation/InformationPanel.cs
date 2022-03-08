using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    [Header("Шаблоны страниц")]
    [SerializeField] private PageStory[] pagesWithStory; // category = 1
    [SerializeField] private PageQuests[] pagesWithQuests; // category = 2
    [SerializeField] private PageItemInformation[] pagesWithGameInformation; // category = 3

    [Header("Страница для отображение информации")]
    [SerializeField] private GameObject pageStory_obj;
    [SerializeField] private GameObject pageQuests_obj;
    [SerializeField] private GameObject pageItemInfo_obj;

    [Header("Номер страницы в UI")]
    [SerializeField] private Text pageNum_UI;

    // приватные переменные
    int page;
    int category;
    int maxPagesInCategory;

    private void OnEnable()
    {
        ChangeCategory(1);
    }

    public void ChangeCategory(int value)
    {
        category = value;
        page = 0;

        switch (category)
        {
            case 1:
                maxPagesInCategory = pagesWithStory.Length - 1;

                pageStory_obj.SetActive(true);
                pageQuests_obj.SetActive(false);
                pageItemInfo_obj.SetActive(false);
                break;
            case 2:
                maxPagesInCategory = pagesWithQuests.Length - 1;

                pageStory_obj.SetActive(false);
                pageQuests_obj.SetActive(true);
                pageItemInfo_obj.SetActive(false);
                break;
            case 3:
                maxPagesInCategory = pagesWithGameInformation.Length - 1;

                pageStory_obj.SetActive(false);
                pageQuests_obj.SetActive(false);
                pageItemInfo_obj.SetActive(true);
                break;
        }

        loadDataToPage();
    }

    private void OpenPage(int value)
    {
        page = value;

        loadDataToPage();
    }

    public void NextPage()
    {
        if (page + 1 <= maxPagesInCategory)
        {
            page++;
        }
        else
        {
            page = 0;
        }

        loadDataToPage();
    }

    public void PreviousPage()
    {
        if(page - 1 >= 0)
        {
            page--;
        }
        else
        {
            page = maxPagesInCategory;
        }

        loadDataToPage();
    }

    private void loadDataToPage()
    {
        switch (category)
        {
            case 1:
                pageStory_obj.transform.FindChild("Text").GetComponent<Text>().text = pagesWithStory[page]._descrition;
                break;
            case 2:
                Debug.Log(page);
                break;
            case 3:
                pageItemInfo_obj.transform.FindChild("ItemName").GetComponent<Text>().text = pagesWithGameInformation[page]._name;
                pageItemInfo_obj.transform.FindChild("ItemName").transform.FindChild("ItemSprite").GetComponent<Image>().sprite = pagesWithGameInformation[page]._sprite;

                string[] craftComponents = pagesWithGameInformation[page].itemsForCraft;
                string craft;

                if(!pagesWithGameInformation[page].isCrafting)
                {
                    craft = "Отсутсвует Крафт";
                }
                else
                {
                    craft = "Крафт: " + craftComponents[0] + " + " + craftComponents[1];
                }

                pageItemInfo_obj.transform.FindChild("ItemDesc").GetComponent<Text>().text = pagesWithGameInformation[page]._description + "\n\n" + craft;

                break;
        }

        pageNum_UI.text = page.ToString();
    }

    public void OpenItemInInfoPanel(Sprite itemSprite)
    {
        for (int i = 0; i < pagesWithGameInformation.Length; i++)
        {
            if(pagesWithGameInformation[i]._sprite == itemSprite)
            {
                gameObject.SetActive(true);
                ChangeCategory(3);
                OpenPage(i);
                break;
            }
        }
    }
}
