using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public int catchedRobbers;
    public bool isDriverInside;

    [SerializeField] GameObject carBorders;
    [SerializeField]
    CarParts carParts;
    Stars stars;
    WheelRotate wheels;

    private void Start()
    {
       // carParts = GetComponentInChildren<CarParts>();
        stars = GetComponentInChildren<Stars>();
        stars.gameObject.SetActive(false);
        wheels = GetComponent<WheelRotate>();
    }

    public void DisableCarBorders()
    {
        carBorders.SetActive(false);
    }

    public void EnableStars()
    {
        stars.gameObject.SetActive(true);
    }

    public void EnableWheels()
    {
        wheels.enabled = true;
    }

    public void DisableWheels()
    {
        wheels.enabled = false;
    }

    public void DriverSit()
    {
        isDriverInside = true;
        carParts.UpdateWindows(catchedRobbers,isDriverInside);
    }

    public void DriverLeave()
    {
        isDriverInside = false;
        carParts.UpdateWindows(catchedRobbers, isDriverInside);
    }

    public void AddColor(CatchingRobbers.ColorType type)
    {
        stars.AddColor(type);
        catchedRobbers++;
        carParts.UpdateWindows(catchedRobbers, isDriverInside);
    }

    public void ClearStars()
    {
        stars.Clear();
    }

    public void LeaveCarAll()
    {
        catchedRobbers = 0;
        isDriverInside = false;
        carParts.UpdateWindows(catchedRobbers, isDriverInside);
    }
}
