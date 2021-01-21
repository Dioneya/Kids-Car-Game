using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWin : MonoBehaviour
{
    Level level;
    PoliceLevel policeLevel;
    BaloonGenerator baloonGenerator;
   

    private void Awake()
    {
        baloonGenerator = FindObjectOfType<BaloonGenerator>();
        policeLevel = GetComponent<PoliceLevel>();
        level = GetComponent<Level>();

        baloonGenerator.enabled = false;
        baloonGenerator.OnTimeFinihed += Finished;
    }

    private void Finished()
    {
        policeLevel.LeavePrisonByWin();
    }

    public void Win()
    {
        policeLevel.ClickOnInteractive();
        level.TurnVictoryMusic();
        policeLevel.Init();
        baloonGenerator.enabled = true;
    }
}
