using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleBase : MonoBehaviour, IInteractibles
{
    public GameObject player { get; set; }

    public UnityEvent OnInteract;

    bool canInteract = false;

    [SerializeField] GameObject icon;

    public virtual void Interact() {}

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (InputManager.Input.Input.Interact.WasPressedThisFrame() && canInteract)
        {
            Interact();
            OnInteract.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == player.transform)
        {
            canInteract = true;
            icon?.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player.transform)
        {
            canInteract = false;
            icon?.SetActive(false);
        }
    }
}
