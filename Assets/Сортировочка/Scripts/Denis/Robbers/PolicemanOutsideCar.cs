using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanOutsideCar : MonoBehaviour
{
    [SerializeField]
    private GameObject policeman;
    [SerializeField]
    private Car _car;
    [SerializeField] private Sprite _outsideSpite;
    private SpriteRenderer spriteRenderer;

    public void CreatePoliceman()
    {
        policeman = FindObjectOfType<Level>().character.gameObject;
        policeman.SetActive(true);
        policeman.GetComponent<CharacterMovement>().Stop();
        policeman.GetComponent<CharacterAnimations>().Swit();
        policeman.transform.position = new Vector3(-3.32f, -2.7f, 0);
        spriteRenderer = policeman.GetComponent<SpriteRenderer>();
        policeman.GetComponent<SoundsPoliceman>().PlayWhistle();
        policeman.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        
        _car.DriverLeave();
    }
    public void DeletPolicMan()
    {
        policeman.SetActive(false);
        policeman.GetComponent<SpriteRenderer>().flipX = false;
        policeman.transform.localScale = new Vector3(1, 1, 1);
        _car.DriverSit();

    }
}
