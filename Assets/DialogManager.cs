using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    Message[] currentMessages;
    Actor[] currentActors;
    int currentMessageIndex;

    public Image Icon;
    public Text MessageText;
    public Text NameText;

    public GameObject DialogPanel;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NextMessage();
        }
    }

    public void StartDialogue(Message[] messages, Actor[] actors)
    {
        DialogPanel.SetActive(true);

        currentMessages = messages;
        currentActors = actors;
        currentMessageIndex = 0;

        DisplayMessage();
    }

    public void DisplayMessage()
    {
        if (currentMessageIndex >= currentMessages.Length)
        {
            DialogPanel.SetActive (false);
            return;
        }

        Icon.sprite = currentActors[currentMessages[currentMessageIndex].actorID].sprite;

        NameText.text = currentActors[currentMessages[currentMessageIndex].actorID].name;
        MessageText.text = currentMessages[currentMessageIndex].message;

        currentMessageIndex++;
    }

    public void NextMessage()
    {
        DisplayMessage();
    }
}
