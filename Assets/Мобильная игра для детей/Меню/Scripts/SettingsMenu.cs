using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles;

    private Settings.Difficult dif;
    void OnEnable()
    {
        toggles[(int)Settings.difficult].isOn = true;
    }
    public void ChangeLvlToEasy()
    {
        dif = Settings.Difficult.Easy;
    }

    public void ChangeLvlToNormal()
    {
        dif = Settings.Difficult.Medium;
    }

    public void ChangeLvlToHard()
    {
        dif = Settings.Difficult.Hard;
    }
    public void CancelSettings()
    {
        dif = Settings.difficult;
    }
    public void ApplySettings()
    {
        if (dif != Settings.difficult)
            Settings.difficult = dif;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
