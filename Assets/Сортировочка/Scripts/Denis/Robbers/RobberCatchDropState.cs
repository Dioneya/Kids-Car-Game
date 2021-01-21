using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberCatchDropState : IRobberState
{
    public void CathingState(FillingRobber fillingRobber)
    {
        fillingRobber.SetSprite(fillingRobber.CathSprite);
        fillingRobber.RobbersSound.PlayAudioCatching();
        fillingRobber.RobbersSound.PlayTimerAudio(true);
        fillingRobber.CatchRobbers.Collider2D.enabled = false;
        fillingRobber.DragAndDrop.IsCanDrag = true;
    }

    public void DropInInteractive(FillingRobber fillingRobber)
    {
        fillingRobber.RobbersSound.InsideInteractive();
        fillingRobber.RobbersSound.PlayTimerAudio(true);
        Vibration.Vibrate(100);
    }

    public void DropAllRobber(FillingRobber fillingRobber)
    {
        throw new System.NotImplementedException();
    }

    public void DropState(FillingRobber fillingRobber)
    {
        fillingRobber.SetSprite(fillingRobber.DefaultSprite);
        fillingRobber.RobbersSound.PlayAudioRobberLose();
        fillingRobber.RobbersSound.PlayTimerAudio(false);
        fillingRobber.WaitTimeToEnableColider();
        fillingRobber.DragAndDrop.IsCanDrag = false;
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
