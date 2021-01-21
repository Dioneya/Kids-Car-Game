using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberFindState : IRobberState
{
    public void CathingState(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }
    public void ReleaseState(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }
    public void FindState(FillingRobber fillingRobber)
    {
        fillingRobber.DragAndDrop.IsCanDrag = true;
        if (!fillingRobber.IsFind) {
            fillingRobber.RobbersSound.PlayAudioFind();
            fillingRobber.IsFind = true;
        }
    }

    public void DropState(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }
    public void DropInInteractive(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }

    public void DropAllRobber(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }
}
