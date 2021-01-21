using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutCharInteraction : MonoBehaviour, IInteractiveAction
{
    [SerializeField] private FillingWindow fillingWindow;
    public void Action()
    {
        StartCoroutine(ActionWithDelay());
    }
    IEnumerator ActionWithDelay()
    {
        fillingWindow.StopSecondPrompt();
        var anim = FindObjectOfType<CharacterAnimations>();
        anim.EatDonut();
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(9);
        fillingWindow.EatDonut();

    }
    public void TurnOff()
    {
        throw new System.NotImplementedException();
    }

    public void TurnOn()
    {
        throw new System.NotImplementedException();
    }
}
