using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject questPanel;
    private List<int> clickedButtonIndices = new List<int>();

    private string[] animals = { "Wolf", "Bear", "Cattle" };
    private string[] description = { "Venture out into the wild and kill ", "Hunt ", "Kill " };//, "Shoot "};
    private int iRandom;


    public List<TextMeshProUGUI> descText;
    public List<GameObject> buttons;
    public TextMeshProUGUI title;

    private bool questActive = false;


    public void Start()
    {
        title.text = "On The Hunt";
        for (int i = 0; i < descText.Count; i++)
        {
            DisplayQuestDescription(i);

        }

        questPanel.SetActive(questActive);

    }

   
    public void CompleteQuest(int index)
    {
        DisplayQuestDescription(index);
    }


    public void ButtonClicked(int index)
    {
        if (!clickedButtonIndices.Contains(index))
        {
            clickedButtonIndices.Add(index);
            Debug.Log("Button " + index + " was clicked.");

        }

        CompleteQuest(index);
    }


    // Display different description for each quests
    public void DisplayQuestDescription(int index)
    {
        if (string.IsNullOrWhiteSpace(descText[index].text))
        {
            descText[index].text = description[index] + (index+1) + " " + animals[0];
        }
        else // Change the description when button is clicked
        {
            iRandom = Random.Range(0, description.Length);
            descText[index].text = description[iRandom];

            iRandom = Random.Range(1, 3);
            descText[index].text += iRandom + " " + animals[0];
        }

    }

    public void questWindow()
    {
        questActive = !questActive;
        questPanel.SetActive(questActive);
        
    }
}
