using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotActive : IWindowState
{
    public void Active(FillingWindow filling)
    {
        filling.Donut.gameObject.SetActive(false);
        filling.ShowAnimation(true);
        filling.WindowCollider.enabled = true;
        //filling.CarClick.Collider2D.enabled = true;
        filling.DreamSpawner.Block();
        filling.StartFirstPrompt();
        filling.StopThreePrompt();
    }
    
}
