using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpilogueTrigger : MonoBehaviour
{
    public string Epilogue;
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p)
        {
            OpenEpilogue();
            gameObject.SetActive(false);
        }
    }

    void OpenEpilogue()
    {
        FindObjectOfType<EpilogueManager>().StartEpilogue(Epilogue);
    }
}
