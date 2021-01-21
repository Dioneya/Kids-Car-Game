using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDonut : MonoBehaviour , IDonutInteractable
{
    [SerializeField] private FillingWindow fillingWindow;
    public void ClickOnInteractive()
    {
        if (fillingWindow != null)
        {
            fillingWindow.State.Active(fillingWindow);
        }
    }
}
