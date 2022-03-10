using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    [Header("������� �������")]
    [SerializeField] private PageStory[] pagesWithStory; // category = 1
    [SerializeField] private PageQuests[] pagesWithQuests; // category = 2
    [SerializeField] private PageItemInformation[] pagesWithGameInformation; // category = 3

    [Header("�������� ��� ����������� ����������")]
    [SerializeField] private GameObject pageStory_obj;
    [SerializeField] private GameObject pageQuests_obj;
    [SerializeField] private GameObject pageItemInfo_obj;

    [Header("����� �������� � UI")]
    [SerializeField] private Text pageNum_UI;

    // ��������� ����������
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
                pageStory_obj.transform.Find("Text").GetComponent<Text>().text = pagesWithStory[page]._descrition;
                break;
            case 2:
                Debug.Log(page);
                break;
            case 3:
                pageItemInfo_obj.transform.Find("ItemName").GetComponent<Text>().text = pagesWithGameInformation[page]._name;
                pageItemInfo_obj.transform.Find("ItemName").transform.Find("ItemSprite").GetComponent<Image>().sprite = pagesWithGameInformation[page]._sprite;

                string[] craftComponents = pagesWithGameInformation[page].itemsForCraft;
                string craft;

                if(!pagesWithGameInformation[page].isCrafting)
                {
                    craft = "���������� �����";
                }
                else
                {
                    craft = "�����: " + craftComponents[0] + " + " + craftComponents[1];
                }

                pageItemInfo_obj.transform.Find("ItemDesc").GetComponent<Text>().text = pagesWithGameInformation[page]._description + "\n\n" + craft;

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
