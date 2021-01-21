using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutDream : MonoBehaviour
{
    UI ui;
    Level level;
    Donut donut;
    DreamSpawner dream;

    private void Start()
    {
        ui = FindObjectOfType<UI>();
        level = FindObjectOfType<Level>();
        donut = level.GetComponent<Donut>();
        dream = FindObjectOfType<DreamSpawner>();
        if (donut == null)
        {
            donut = level.gameObject.AddComponent<Donut>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DragAndDrop>() != null)
        {
            ButtonClicked();
        }
    }

    private void ButtonClicked()
    {
        dream.DisableDream();
        donut.GenerateDonutBuilding();
        ui.TurnOffGameplayUI();
    }
}
