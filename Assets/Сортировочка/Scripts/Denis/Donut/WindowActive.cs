using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowActive : IWindowState
{
    public void Active(FillingWindow filling)
    {
        filling.Donut.gameObject.transform.localPosition = filling.PositionDonut;
        filling.Donut.enabled = true;
        filling.Donut.gameObject.SetActive(true);
        filling.BuySound.Play();
        filling.ShowAnimation(false);
        filling.CreateMoney();
        filling.WindowCollider.enabled = false;
        filling.CarClick.Collider2D.enabled = false;
        filling.StopFirstPrompt();
        filling.StopThreePrompt();
        filling.StartSecondPrompt();
       
    }
}
