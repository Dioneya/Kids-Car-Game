using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpeed : MonoBehaviour
{
    public event Action OnSpeedChanged;
    public event Action OnCarStop;
    public int currentState { get; private set; }
    private int cashedState = 0;

    [SerializeField] List<float> speedStates;

    
    private LevelMover level;
    private Car car;

    private void Awake()
    {
        car = FindObjectOfType<Car>();
        level = GetComponent<LevelMover>();
    }

    public void Stop()
    {
        OnCarStop?.Invoke();
        level.SetSpeed(0);
    }

    public void ReturnAfterStop()
    {
        SetSpeedLevel(cashedState,true);
    }

    public void SetSpeedLevel(int num,bool isCashingState)
    {
        if (num >= speedStates.Count)
        {
            Debug.Log("No such speed");
            return;
        }
        if(isCashingState)
            cashedState = currentState;
        currentState = num;
        level.SetSpeed(speedStates[currentState]);

        OnSpeedChanged?.Invoke();
    }

    public void NextSpeed()
    {
        currentState++;
        if (currentState >= speedStates.Count)
            currentState = 0;
        level.SetSpeed(speedStates[currentState]);

        OnSpeedChanged?.Invoke();
    }

    

}
