using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractibles
{
    public GameObject player { get; set; }

    public void Interact();
}
