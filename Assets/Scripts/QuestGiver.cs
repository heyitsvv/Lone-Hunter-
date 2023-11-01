using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerMovement player;

    private string[] animals = { "Wolf", "Bear", "Cattle" };
    private string[] Title = { "Next Quest", "Hunting Challenge", "On The Hunt" };
    private int index;
    private int numberKill;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;

    public void Start()
    {
        titleText.text = quest.title;
        descText.text = quest.description;
        index = 0;
    }

    public void CompleteQuest()
    {
        index = Random.Range(0, animals.Length);
        numberKill = Random.Range(1, 2);
        titleText.text = Title[index];
        descText.text = "Venture out into the wild and kill " + numberKill + " " + animals[index];

    }
}
