using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBuildings : MonoBehaviour
{
    GameObject car;
    IInteractiveBuilding interactive;
    [SerializeField] float distanceToStop = 5f;
    [SerializeField] GameObject markerToStop = null;
    public bool isTrigger=false; 
    public bool hasRoad = true;
    [SerializeField] GameObject customeRoad; 
    void Awake() 
    {
        interactive = GetComponent<IInteractiveBuilding>();
    }
    void Start()
    {
        car = GameObject.Find("CarBase");
        LevelManager levelManager = LevelManager.GetLevelManager();
        if (hasRoad && customeRoad!=null) 
        {
            LvlGenerator gen = GetComponent<MoveEnviroment>().GetGenerator();
            gen.GenerateRoadInteractive(gameObject, customeRoad);
        }        
    }

    private void StartInteract() 
    {
        LevelManager.isMoved = false;
        car.GetComponent<CarV2>().CarStop();
        interactive.Action();
        isTrigger = false;
    }
    public GameObject GetMarkerToStopObj() 
    {
        return markerToStop;
    }
    void Update() 
    {
        if (isTrigger) 
        {
            if (markerToStop != null && markerToStop.transform.position.x <= car.transform.position.x) 
            {
                Vector3 vector3 = new Vector3(markerToStop.transform.position.x, car.transform.position.y, car.transform.position.z);
                car.transform.position = vector3;
                StartInteract();
            }
            else if (markerToStop == null && gameObject.transform.position.x - car.transform.position.x <= distanceToStop)
            {
                StartInteract();
            }
        }
    }
}
