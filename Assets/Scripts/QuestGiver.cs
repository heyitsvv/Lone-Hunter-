using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerMovement player;

    private string[] animals = { "Wolf", "Cattle" };
    private string[] huntVerbs = { "Venture out into the wild and kill", "Hunt", "Collect meats from",
                                   "Hunt and gather the products from" };
    private string[] gatherVerbs = { "Gather", "Collect", "Retrieve" };
    private string[] locations = { "the forest", "the rock mountains" };

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI levelText;

    private int count = 0;
    private int questNumber = 2;
    private int levelNumber = 1;

    public void Start()
    {
        titleText.text = "Level " + levelNumber;
        levelText.text = "(Level Advancement: " + count + "/" + questNumber + " quest completed)";
        GenerateQuestDescription();
    }

    public void CompleteQuest()
    {
        GenerateQuestDescription();
        count += 1;
        if (count == questNumber)
        {
            levelNumber += 1;
            titleText.text = titleText.text = quest.title + " : Level " + levelNumber;
            count = 0;

            questNumber = Random.Range(3, questNumber+3);
            levelText.text = "(Level Advancement: " + count + "/" + questNumber + " completed)";
        }
        else
        {
            levelText.text = "(Level Advancement: " + count + "/" + questNumber + " completed)";
        }
    }

    // Animal:0, description:1, numberkill:2
    // Another animal:3(Yes), 4(No)
    private int randomNumber(int x)
    {
        if (x == 0)
        {
            return Random.Range(0, animals.Length);
        }
        else if (x == 1)
        {
            return Random.Range(0, huntVerbs.Length);
        }
        else
        {
            return Random.Range(1, 4);
        }
    }

    private void GenerateQuestDescription()
    {
        int animalIndex1 = randomNumber(0);
        int animalIndex2;

        // Ensure that the second animal is different from the first
        do
        {
            animalIndex2 = randomNumber(0);
        } while (animalIndex2 == animalIndex1);

        int verbIndex = randomNumber(1);
        int number1 = randomNumber(2);
        int number2 = randomNumber(2);
        int locationIndex = Random.Range(0, locations.Length);

        // Determine whether to display one or two animals
        string animalsDescription;

        if (randomNumber(3) == 3)
        {
            animalsDescription = $"{number1} {animals[animalIndex1]} and {number2} {animals[animalIndex2]}";
        }
        else
        {
            animalsDescription = (number1 == 1) ? $"{number1} {animals[animalIndex1]}" : $"{number1} {animals[animalIndex1]} and {number2} {animals[animalIndex2]}";
        }

        string description;

        if (verbIndex < huntVerbs.Length)
        {
            description = $"{huntVerbs[verbIndex]} {animalsDescription} in {locations[locationIndex]}.";
        }
        else
        {
            description = $"{gatherVerbs[verbIndex - huntVerbs.Length]} {animalsDescription} from {locations[locationIndex]}.";
        }

        descText.text = description;
    }
}
