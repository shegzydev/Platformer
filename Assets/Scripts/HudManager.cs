using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HudEntry
{
    public HUDData hUDData;
    public Slider slider;
}

public enum HUDData
{
    HP,
    MP,
    XP
}
public class HudManager : MonoBehaviour
{
    public HudEntry[] hudEntry;
    HudEntry[] hudEntries;

    void Start()
    {
        hudEntries = new HudEntry[hudEntry.Length];
        for (int i = 0; i < hudEntry.Length; i++)
        {
            hudEntries[(int)hudEntry[i].hUDData] = hudEntry[i];
        }
    }

    void Update()
    {

    }

    public void UpdateHudEntry(HUDData data, float value)
    {
        hudEntries[(int)data].slider.value = value;
    }
}
