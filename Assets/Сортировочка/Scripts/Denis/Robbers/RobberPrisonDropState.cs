using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberPrisonDropState : IRobberState
{
    public void CathingState(FillingRobber fillingRobber)
    {
        fillingRobber.InitHints();
        fillingRobber.SetSprite(fillingRobber.CathSprite);
        fillingRobber.RobbersSound.PlayAudioCatching();
        fillingRobber.RobbersSound.PlayTimerAudio(true);
        fillingRobber.CatchRobbers.Collider2D.enabled = false;
        fillingRobber.DragAndDrop.IsCanDrag = true;
    }

    public void DropInInteractive(FillingRobber fillingRobber)
    {
        Vibration.Vibrate(100);
        fillingRobber.RobbersSound.InsideInteractive();
        fillingRobber.Hints.StartPosition = null;
        fillingRobber.Hints.EndPosition = null;
    }
    public void DropAllRobber(FillingRobber fillingRobber)
    {
        Vibration.Vibrate(100);
        fillingRobber.RobbersSound.PlayTimerAudio(true);
        fillingRobber.Hints.StartPosition = null;
        fillingRobber.Hints.EndPosition = null;
    }
    public void DropState(FillingRobber fillingRobber)
    { 
        fillingRobber.RobbersSound.PlayAudioRobberLose();
        fillingRobber.RobbersSound.PlayTimerAudio(false);
        fillingRobber.WaitTimeToEnableColider();
        fillingRobber.DragAndDrop.IsCanDrag = true;
        fillingRobber.IsFind = true;
    }

    public void FindState(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }

    public void ReleaseState(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }
}
