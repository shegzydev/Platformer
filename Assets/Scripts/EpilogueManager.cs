using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EpilogueManager : MonoBehaviour
{
    public UnityEvent OnTrigger;
    public Text EpilogueText;

    string currentText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartEpilogue(string epilogue)
    {
        OnTrigger.Invoke();
        currentText = epilogue;
        InputManager.Deactivate();
        StartCoroutine(Routine());
    }
    IEnumerator Routine()
    {
        yield return new WaitForSeconds(1.333333f);
        StartCoroutine(LoadText());
    }

    IEnumerator LoadText()
    {
        for (int i = 0; i < currentText.Length; i++)
        {
            EpilogueText.text += currentText[i];
            yield return null;
        }
    }
}
