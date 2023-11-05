using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Actor[] actors;
    public Message[] messages;

    void Start()
    {
        OpenDialogue();
    }

    public void OpenDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(messages, actors);
    }

    void Update()
    {
        
    }
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class Message
{
    public int actorID;
    public string message;
}