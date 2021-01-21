using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarClick : MonoBehaviour, IDonutInteractable
{
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private CharacterAnimations animationPoliceman;
    [SerializeField] private GameObject polisMan;
    [SerializeField] private FillingWindow fillingWindow;
    [SerializeField] private DreamSpawner _dreamSpawner;
    private CharacterMovement charMover;

    public void Init(DreamSpawner dreamSpawner)
    {
        _dreamSpawner = dreamSpawner;
    }
    public Collider2D Collider2D { get => collider2D; set => collider2D = value; }

    public void ClickOnInteractive()
    {
        fillingWindow = FindObjectOfType<FillingWindow>();
        charMover = FindObjectOfType<CharacterMovement>();
        animationPoliceman.Walk();
        charMover.GoTo(collider2D.gameObject.transform.position);
        charMover.OnTargetReached += SitDownInCar;
        collider2D.enabled = false;
        fillingWindow.WindowCollider.enabled = false;

    }
    private void SitDownInCar()
    {
        fillingWindow.StopThreePrompt();
        fillingWindow.StopSecondPrompt();
        fillingWindow.StopFirstPrompt();
        fillingWindow.ShowAnimation(false);
        FindObjectOfType<Car>().DriverSit();
        var character = FindObjectOfType<Level>().character;
        character.gameObject.SetActive(false);
        charMover.OnTargetReached -= SitDownInCar;
        FindObjectOfType<Car>().DriverSit();
        var levelMover = FindObjectOfType<LevelMover>();
        levelMover.GetComponent<Level>().audio.Play();
        levelMover.ReturnCashedDay();
        FindObjectOfType<UI>().TurnOnGameplayUI(0);
        levelMover.GetComponent<LevelSpeed>().ReturnAfterStop();
        _dreamSpawner.UnBlock(false);
    }
}
