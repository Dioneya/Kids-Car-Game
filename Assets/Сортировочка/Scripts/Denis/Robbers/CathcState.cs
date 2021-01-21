using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CathcState : IRobberState
{
    public void CathingState(FillingRobber fillingRobber)
    {
        fillingRobber.SetSprite(fillingRobber.CathSprite);
        fillingRobber.RobbersSound.PlayAudioCatching();
        fillingRobber.RobbersSound.PlayTimerAudio(true);
        fillingRobber.CatchRobbers.Collider2D.enabled = false;
        fillingRobber.DragAndDrop.IsCanDrag = true;

    }
    public void ReleaseState(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }
    public void FindState(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
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
