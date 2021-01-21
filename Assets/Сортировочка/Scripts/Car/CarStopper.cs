using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStopper : MonoBehaviour
{
    public LevelMover levelMover;
    LevelSpeed levelSpeed;
    Car car;
    IInteractiveBuilding building;

    void Start()
    {
        if(levelMover == null)
        {
            levelMover = FindObjectOfType<LevelMover>();
        }
        car = FindObjectOfType<Car>();
        building = GetComponent<IInteractiveBuilding>();
        levelSpeed = levelMover.GetComponent<LevelSpeed>();
    }

    void LateUpdate()
    {
        var dist = Vector3.Distance(transform.position,car.transform.position);
        if(dist <= 6)
        {
            levelSpeed.Stop();
            car.GetComponentInChildren<CarParts>().winker?.SoundOff();
            levelSpeed.GetComponent<Level>().audio.Pause();
            building.Action();
            
            enabled = false;
        }
    }
}
