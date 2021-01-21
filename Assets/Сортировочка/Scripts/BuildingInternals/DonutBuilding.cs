using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutBuilding : MonoBehaviour, IInteractiveBuilding
{
    GameObject character;
    [SerializeField]
    private FillingWindow _window;
    
    public void Action()
    {
        FindObjectOfType<Car>().DriverLeave();
        character = FindObjectOfType<Level>().character.gameObject;
        character.SetActive(true);
        character.transform.position = new Vector3(4, -2.7f,0);
        character.GetComponent<CharacterAnimations>().Idle();
        character.GetComponent<CharacterMovement>().Stop();
        _window.NotActive();
    }
}
