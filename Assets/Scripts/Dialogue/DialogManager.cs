using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    Message[] currentMessages;
    Actor[] currentActors;
    UnityEvent onEndDialogue;

    int currentMessageIndex;

    public Image Icon;
    public Text MessageText;
    public Text NameText;

    public GameObject DialogPanel;
    public UnityEvent OnStartDialogue;

    bool dialogOpened;
    bool writing;
    void Start()
    {

    }

    void Update()
    {
        if (InputManager.Input.Dialog.Skip.WasPressedThisFrame() && dialogOpened && !writing)
        {
            NextMessage();
        }
    }

    public void StartDialogue(Message[] messages, Actor[] actors, UnityEvent endEvent)
    {
        InputManager.Deactivate();

        DialogPanel.SetActive(true);
        OnStartDialogue.Invoke();

        dialogOpened = true;

        currentMessages = messages;
        currentActors = actors;
        onEndDialogue = endEvent;

        currentMessageIndex = 0;

        DisplayMessage();
    }

    public void DisplayMessage()
    {
        if (currentMessageIndex >= currentMessages.Length)
        {
            InputManager.Activate();
            onEndDialogue.Invoke();
            DialogPanel.SetActive(false);
            dialogOpened = false;
            return;
        }

        writing = true;
        Icon.sprite = currentActors[currentMessages[currentMessageIndex].actorID].sprite;
        NameText.text = currentActors[currentMessages[currentMessageIndex].actorID].name;

        StartCoroutine(WritingRoutine());
        //MessageText.text = currentMessages[currentMessageIndex].message;
    }

    public void NextMessage()
    {
        DisplayMessage();
    }

    IEnumerator WritingRoutine()
    {
        MessageText.text = "";
        for (int i = 0; i < currentMessages[currentMessageIndex].message.Length; i++)
        {
            MessageText.text += currentMessages[currentMessageIndex].message[i];
            yield return null;
        }
        writing = false;
        currentMessageIndex++;
    }
}
