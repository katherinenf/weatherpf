using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // The array of tutorial pages
    public GameObject[] pages;

    // The text of the next button gui
    public Text nextButtonText;

    // The previous button gui
    public Button prevButton;

    // The page fraction text
    public Text pageText;

    int currentPage;

    public void Start()
    {
        UpdatePages();
        UpdateButtons();
    }

    public void NextPage()
    {
        if (currentPage + 1 < pages.Length)
        {
            currentPage++;
            UpdatePages();
            UpdateButtons();
        }
        else
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePages();
            UpdateButtons();
        }
    }

    void UpdatePages()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(currentPage == i);
        }
    }
    
    void UpdateButtons()
    {
        prevButton.interactable = (currentPage > 0);
        nextButtonText.text = (currentPage + 1 >= pages.Length) ? "Menu" : "Next";
        pageText.text = "[" + (currentPage + 1).ToString() + "/" + pages.Length.ToString() + "]";
    }
}
