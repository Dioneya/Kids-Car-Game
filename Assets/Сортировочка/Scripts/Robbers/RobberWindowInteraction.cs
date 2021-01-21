using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberWindowInteraction : MonoBehaviour,IInteractiveAction
{
    private Robber _robber;
    private GameObject _target;
    public GameObject Target => _target;
    private string _colorName;
    public string ColorName => _colorName;

    [SerializeField] private PartSettings _partSettings;
    private void Init()
    {
        if (_robber == null)
        {
            _robber = GetComponent<Robber>();
            _robber.FollowOnEvent();
            _robber.DropState = new RobberPrisonDropState();
            _robber.GetComponent<FillingRobber>().RobbersSound.PlayTimerAudio(false);
        }
    }

    public RobberWindowInteraction InitColor(string color)
    {
        _colorName = color;
        Init();
        return this;
    }
    public void InitTarget(GameObject transform)
    {
        _partSettings = GetComponent<PartSettings>();
        _target = transform;
        _robber.InitPosition(_target.transform);
        _partSettings.destinationObjectTag = _target.name;

    }
    void IInteractiveAction.Action()
    {
        var prison = FindObjectOfType<Prison>();
        prison.FillWindow(_target);
        var catching = FindObjectOfType<CatchingRobbers>();
        catching.robbers.Remove(gameObject);
        _robber.DropInInterctive();
        if (catching.robbers.Count == 0)
        {
            catching.GetComponent<PoliceLevel>().LeavePrison();
            print(catching.robbers.Count);
            _robber.DropAllRobber();
        }
        else
        {
            _robber.MuteTimer(false);
        }
        Destroy(gameObject);
    }
  

    void IInteractiveAction.TurnOff()
    {
        throw new System.NotImplementedException();
    }

    void IInteractiveAction.TurnOn()
    {
        throw new System.NotImplementedException();
    }
    
}
