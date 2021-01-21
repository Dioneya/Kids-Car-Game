﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberCarInteraction : MonoBehaviour,IInteractiveAction
{
    [SerializeField] private PolicemanOutsideCar policman;
    private void Start()
    {
        policman = FindObjectOfType<PolicemanOutsideCar>();
    }
    public void Action()
    {

        var robber = GetComponent<Robber>();
      //robber.DropState = new RobberCatchDropState();
        robber.DropInInterctive();
        policman.DeletPolicMan();
        var catching = FindObjectOfType<CatchingRobbers>();
        var color = catching.GetCurrentColor();
        var car = FindObjectOfType<Car>();
        var levelMover = FindObjectOfType<LevelMover>();
        Debug.Log("Robber in the car");
        car.AddColor(color);
        catching.catchedRobbers.Add(color);
        catching.ChangeUi();
        levelMover.SwapBackLayers();
        levelMover.GetComponent<Level>().audio.Play();
        car.GetComponentInChildren<CarParts>().TurnOffAll();
        levelMover.ReturnCashedDay();
        catching.DeleteLight();
        FindObjectOfType<UI>().TurnOnGameplayUI(3);
        levelMover.GetComponent<LevelSpeed>().ReturnAfterStop();
        FindObjectOfType<DreamSpawner>().UnBlock(false);
        Destroy(gameObject);
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
