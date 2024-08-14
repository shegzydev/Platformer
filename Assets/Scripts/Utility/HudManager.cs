using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Entry
{
    public Slider slider;
    public Text text;
}

public class HudManager : MonoBehaviour
{
    public Entry HudSlot;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void UpdateHud(float value)
    {
        HudSlot.slider.value = value;
    }
}
