using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleBase : MonoBehaviour, IInteractibles
{
    public GameObject player { get; set; }

    public UnityEvent OnInteract;

    public virtual void Interact() {}

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == player.transform)
        {
            Interact();
            OnInteract.Invoke();
        }
    }
}
