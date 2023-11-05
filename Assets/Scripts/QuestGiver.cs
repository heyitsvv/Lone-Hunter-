using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerMovement player;

    private string[] animals = { "Wolf"};
    private string[] Title = { "Next Quest", "Hunting Challenge", "On The Hunt" };
    private int index;
    private int numberKill;
    private bool isQuestOpen;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;

    public void Start()
    {
        isQuestOpen = false;
        titleText.text = quest.title;
        descText.text = quest.description;
        index = 0;
    }

    public void CompleteQuest()
    {
        index = Random.Range(0, animals.Length);
        numberKill = Random.Range(1, 4);
        titleText.text = Title[index];
        descText.text = "Venture out into the wild and kill " + numberKill + " " + animals[0];

    }


    public void questWindow (bool state)
    {
        isQuestOpen = state;
    }

    public void ToggleCursorState()
    {
        if (isQuestOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
