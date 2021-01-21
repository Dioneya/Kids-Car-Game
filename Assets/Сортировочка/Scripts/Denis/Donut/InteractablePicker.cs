using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePicker : MonoBehaviour
{
    private IDonutInteractable donutInteractable;
    private void Start()
    {
        donutInteractable = GetComponent<IDonutInteractable>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DragAndDrop>() != null)
        {
            donutInteractable.ClickOnInteractive();
        }
    }
}
