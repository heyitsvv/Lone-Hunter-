using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour
{
    public int buttonIndex;

    public QuestGiver questGiver;

    private void Start()
    {
        Button button = GetComponent<Button>();

    
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        questGiver.ButtonClicked(buttonIndex);
    }


}
