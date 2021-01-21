using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingRobbers : MonoBehaviour
{
    LevelMover levelMover;
    LevelSpeed levelSpeed;
    [SerializeField]
    LevelGenerator levelGenerator;
    PoliceLevel policeLevel;
    DreamSpawner dreamSpawner;

    CarParts carParts;
    public Car car;
    public event Action OnColorsChanged;
    public GameObject[] robbersPrefab;

    public List<GameObject> robbers;
    public List<ColorType> catchedRobbers;
    public List<ColorType> types;
    public int currentTypeCatching;

    public GameObject lightPrefab;
    public GameObject light;

    private bool isCatching = false;
    private bool isPrison = false;
    //private List<ColorType> availableTypes;

    private void Start()
    {
        policeLevel = GetComponent<PoliceLevel>();
        robbersPrefab = Resources.LoadAll<GameObject>("Levels/Police/Robbers");
        types = new List<ColorType>();
        catchedRobbers = new List<ColorType>();
        RandomizeColors();
        carParts = FindObjectOfType<CarParts>();
        car = carParts.GetComponentInParent<Car>();
        levelMover = GetComponent<LevelMover>();
        levelGenerator = GetComponent<LevelGenerator>();
        levelSpeed = GetComponent<LevelSpeed>();
        lightPrefab = levelGenerator.LightGameObject;
        
        dreamSpawner = FindObjectOfType<DreamSpawner>();
    }

    public void Action()
    {
        if (isCatching)
        {
            Debug.Log("Catching");
        }
        if (isPrison)
        {
            Debug.Log("Prison");
        }
    }

    public void StartCatch(int index)
    {
        isPrison = false;
        isCatching = true;
        currentTypeCatching = index;
        levelMover.TurnNight(false);
        levelMover.SwapLayers();
        carParts.TurnAll();
        levelGenerator.SpawnExceptUniq(new string[] { "осн.кафе" });
        levelSpeed.SetSpeedLevel(2,true);
        try
        {
            dreamSpawner.Block();
        }
        catch 
        {

            
        }
        
    }

    public void ToPRison()
    {
        isPrison = true;
        isCatching = false;
        levelMover.TurnTransparent();
        levelMover.TurnDay(false);
        levelMover.SwapBackLayers();
        carParts.TurnOffAll();
        levelGenerator.SpawnAsUniq("Levels/Police/Environment/Prison/Prison");
        levelSpeed.SetSpeedLevel(2,true);
        dreamSpawner.Block();
    }
    
   

    public void DeleteLight()
    {
        // Destroy(light);
        light.SetActive(false);
    }

    public void RandomizeColors()
    {
        types = policeLevel.Get3RandomColors();
        Debug.Log(types.Count);
        OnColorsChanged?.Invoke();

    }

    public void ChangeUi()
    {
        OnColorsChanged?.Invoke();
    }

    public ColorType GetCurrentColor()
    {
        return types[currentTypeCatching];
    }

    public enum ColorType { Green,Red,Orange,Yellow,Blue}

}
