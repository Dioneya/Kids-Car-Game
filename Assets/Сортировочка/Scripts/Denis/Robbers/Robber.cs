using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FillingRobber))]
public class Robber : MonoBehaviour
{
    private IRobberState _state;
    private IDropStateRobber _dropState;
    private FillingRobber _fillingRobber;
    private DragAndDrop dragAndDrop;
    public IDropStateRobber DropState { get => _dropState; set => _dropState = value; }
    private void Start()
    {
        _fillingRobber = GetComponent<FillingRobber>();
    }
    private void OnDestroy()
    {
        if (dragAndDrop != null)
        {
            dragAndDrop.OnFail -= Drop;
            dragAndDrop.OnSuccess -= DropInInterctive;
        }
    }
    public void FollowOnEvent()
    {
        dragAndDrop = FindObjectOfType<DragAndDrop>();
        dragAndDrop.OnFail += Drop;
        dragAndDrop.OnSuccess += DropInInterctive;
    }
    public void Catching()
    {
        if (_dropState != null)
            _dropState.CathingState(_fillingRobber);
        else print("null");
    }
    public void MuteTimer(bool mute)
    {
        _fillingRobber.RobbersSound.PlayTimerAudio(mute);

    }
    public void Find()
    {
        _state = new RobberFindState();
        _state.FindState(_fillingRobber);
    }
    public void Release()
    {
        _state = new RobberLoseState();
        _state.ReleaseState(_fillingRobber);
    }
    public void Drop()
    {
        if(_dropState!=null)
            _dropState.DropState(_fillingRobber);
        else print("null");
    }
    public void DropInInterctive()
    {
        if (_dropState != null)
            _dropState.DropInInteractive(_fillingRobber);
        else print("null");
    }
    public void DropAllRobber()
    {
        _dropState = new RobberPrisonDropState();
        _dropState.DropAllRobber(_fillingRobber);
    }
    public void InitPosition(Transform window)
    {
        if (_fillingRobber != null)
        {
            _fillingRobber.EndPosition = window;
        }
        else
        {
            _fillingRobber = GetComponent<FillingRobber>();
            _fillingRobber.EndPosition = window;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<LightCatchRobbers>() != null)
        {
            return;
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<LightCatchRobbers>() != null)
        {
            return;
        }
    }
}
