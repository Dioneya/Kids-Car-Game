using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PoliceLevel : MonoBehaviour
{
    private LevelSettings.Difficult difficultLevel;

    public List<CatchingRobbers.ColorType> windowColor;
    public List<int> windowPlace;
    public List<bool> windowState;

    public Sprite[] emptyWindows;
    public Sprite[] filledWindows;
    [SerializeField]
    private CharacterAnimations animationPoliceman;
    [SerializeField]
    private CharacterMovement charMover;
    [SerializeField]
    private Car car;
    private bool InCar;
    private void Awake()
    {
        difficultLevel = LevelSettings.Instance.difficultLevel;
        Init();

        emptyWindows = Resources.LoadAll<Sprite>("Levels/Police/Windows/Empty");
        filledWindows = Resources.LoadAll<Sprite>("Levels/Police/Windows/Filled");

    }

    /*
     * prison windows order
     * 
     * 0 1  4 5 6
     * 2 3  7 8 9
     */
    public void Init()
    {

        windowColor = new List<CatchingRobbers.ColorType>();
        windowPlace = new List<int> ();
        windowState = new List<bool>();

        List<int> keys = new List<int>() ;
        for(int i = 0; i < 10; i++)
        {
            keys.Add(i);
        }

        var count = ((int)difficultLevel + 1) * 3;

        for(int i = 0; i < count; i++)
        {
            var r = 0;
            var key = Random.Range(0, keys.Count);
            key = keys[key];
            keys.Remove(key);
            windowPlace.Add(key);
            //if(key < 4)
            {
            //    r = Random.Range(0, 2);
            }
            //else
            {
            //    r = Random.Range(2, 5);
            }
            r = Random.Range(0, 5);
            windowColor.Add((CatchingRobbers.ColorType) r);
            windowState.Add(true);
        }
    }

    public List<CatchingRobbers.ColorType> Get3RandomColors()
    {
        List<int> buffer = new List<int>();
        List<CatchingRobbers.ColorType> result = new List<CatchingRobbers.ColorType>();

        for (int i = 0;i< windowState.Count;i++)
        {
            if (windowState[i])
            {
                buffer.Add(i);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            //if(true)
            if (buffer.Count == 0)
            {
                return result;
            }
            var r = Random.Range(0, buffer.Count);
            var futColor = windowColor[buffer[r]];
            if (result.Contains(futColor))
            {
                var colorIndexToChange = buffer[r];
                futColor = (CatchingRobbers.ColorType) RandomExcept(0,5,(int)futColor,result);
                windowColor[colorIndexToChange] = futColor;
                
            }
            result.Add(futColor);
            buffer.RemoveAt(r);
        }
        Debug.Log(result.Count);
        return result;
    }

    private bool CheckAllCatched()
    {
        List<int> buffer = new List<int>();

        for (int i = 0; i < windowState.Count; i++)
        {
            if (windowState[i])
            {
                buffer.Add(i);
            }
        }
        if (buffer.Count == 0)
        {
            GetComponent<LevelWin>().Win();
            return true;
        }
        return false;
    }

    public int RandomExcept(int min, int max, int except,List<CatchingRobbers.ColorType> exceptList)
    {
        var res = except;
        while (res == except || exceptList.Contains((CatchingRobbers.ColorType)res))
        {
            res = Random.Range(min, max);
        }
        return res;
    }

    public void LeavePrison()
    {
        if (!CheckAllCatched())
        {
            StartCoroutine(GoOutPricon());
        }
    }
    public void LeavePrisonByWin()
    {
        StartCoroutine(GoOutPrisonByWin());
    }

    IEnumerator GoOutPrisonByWin()
    {
        var levelMover = GetComponent<LevelMover>();
        var speed = levelMover.GetComponent<LevelSpeed>();
        levelMover.GetComponent<Level>().TurnDriveMusic();
       // FindObjectOfType<Car>().DriverSit();
       // var policeman = FindObjectOfType<Level>().character;
       // policeman.transform.position = new Vector3(0, -10, 0);
        speed.SetSpeedLevel(2, false);
        yield return new WaitForSeconds(7);
        levelMover.TurnOffTransparent(1);
        speed.ReturnAfterStop();
        var catching = GetComponent<CatchingRobbers>();
        catching.RandomizeColors();
        FindObjectOfType<UI>().TurnOnGameplayUI(3);
        FindObjectOfType<DreamSpawner>().UnBlock(true);
    }
    IEnumerator GoOutPricon()
    {
        var levelMover = GetComponent<LevelMover>();
        var speed = levelMover.GetComponent<LevelSpeed>();
        ClickOnInteractive();
        yield return new WaitUntil(()=> IsInCar());
        levelMover.GetComponent<Level>().TurnDriveMusic();
        speed.SetSpeedLevel(2, false);
        yield return new WaitForSeconds(7);
        levelMover.TurnOffTransparent(1);
        speed.ReturnAfterStop();
        var catching = GetComponent<CatchingRobbers>();
        catching.RandomizeColors();
        FindObjectOfType<UI>().TurnOnGameplayUI(3);
        FindObjectOfType<DreamSpawner>().UnBlock(true);
    }

    public void ClickOnInteractive()
    {
        InCar = false;
        animationPoliceman.Walk();
        charMover.GoTo(car.transform.position);
        charMover.OnTargetReached += SitDownInCar;
    }

    private void SitDownInCar()
    {
        FindObjectOfType<Car>().DriverSit();
        charMover.OnTargetReached -= SitDownInCar;
        var policeman = FindObjectOfType<Level>().character;
        policeman.transform.position = new Vector3(0, -10, 0);
        policeman.GetComponent<CharacterAnimations>();
        InCar = true;
    }
    private bool IsInCar()
    {
        return InCar;
    }
}


