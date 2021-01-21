using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDonut : MonoBehaviour
{

    UI ui;
    Level level;
    Donut donut;

    private void Start()
    {
        ui = GetComponentInParent<UI>();
        level = FindObjectOfType<Level>();
        donut = level.GetComponent<Donut>();
        if(donut == null)
        {
            donut = level.gameObject.AddComponent<Donut>();
        }
    }

    public void ButtonClicked()
    {
        donut.GenerateDonutBuilding();
        ui.TurnOffGameplayUI();
    }
}
