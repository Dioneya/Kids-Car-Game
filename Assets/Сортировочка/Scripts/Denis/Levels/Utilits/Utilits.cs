using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilits 
{
    
    public static void TurnOn(IInteractiveAction type , IInteractiveAction[] _interactiveComponents)
    {
        foreach (IInteractiveAction interactive in _interactiveComponents)
        {
            if (interactive.GetType() == type.GetType())
            {
                interactive.TurnOn();
            }
        }
    }
    public static void TurnOff(IInteractiveAction type , IInteractiveAction[] _interactiveComponents)
    {
        foreach (IInteractiveAction interactive in _interactiveComponents)
        {
            if (interactive.GetType() == type.GetType())
            {
                interactive.TurnOff();
            }
        }
    }
    public static void TurnAll(IInteractiveAction[] _interactiveComponents)
    {
        foreach (IInteractiveAction interactive in _interactiveComponents)
        {
            interactive.TurnOn();
        }
    }
    public static void TurnOffAll(IInteractiveAction[] _interactiveComponents)
    {
        foreach (IInteractiveAction interactive in _interactiveComponents)
        {
            interactive.TurnOff();
        }
    }
}
