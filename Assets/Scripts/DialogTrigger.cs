using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    public Actor[] actors;
    public Message[] messages;
    public UnityEvent OnEndDialogEvent;

    void Start()
    {

    }

    public void OpenDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(messages, actors, OnEndDialogEvent);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p)
        {
            OpenDialogue();
            gameObject.SetActive(false);
        }
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