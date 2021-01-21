using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICatchingRobbers : MonoBehaviour
{
    private Level level;
    private CatchingRobbers catching;
    private Car car;
    private UI ui;
    private LevelMover levelMover;

    [SerializeField] List<Button> buttons;
    [SerializeField] Button prison;
    [SerializeField] Sprite[] icons;
    [SerializeField] Hints hints;
    private List<int> bannedButtons;

    private void Start()
    {
        bannedButtons = new List<int>();
        car = FindObjectOfType<Car>();
        ui = GetComponentInParent<UI>();
        level = FindObjectOfType<Level>();
        levelMover = level.GetComponent<LevelMover>();
        catching = level.GetComponent<CatchingRobbers>();
        if (catching== null)
            catching = level.gameObject.AddComponent<CatchingRobbers>();

        catching.OnColorsChanged += ChangeIcons;

        icons = Resources.LoadAll<Sprite>("Levels/Police/BanditIcons");
    }


    public void ChangeIcons()
    {
        bannedButtons.Clear();
        if (catching.types.Count > buttons.Count)
        {
            Debug.Log("Error bandit buttons count");
            return;
        }

        for (int i = 0; i < 3; i++)
            
        {
            if (catching.types.Count > i)
            {
                buttons[i].GetComponent<Image>().sprite = FindIcon(catching.types[i] + "Active");

                if (catching.catchedRobbers.Contains(catching.types[i]))
                {
                    bannedButtons.Add(i);
                    //bannedButtons.RemoveAt(0);
                    buttons[i].GetComponent<Image>().sprite = FindIcon(catching.types[i] + "Passive");
                }
            }
            else
            {

                buttons[i].GetComponent<Image>().sprite = FindIcon((CatchingRobbers.ColorType)Random.Range(0,5) + "Passive");
                bannedButtons.Add(i);
            }
            
        }

        if (car.catchedRobbers == 0)
        {
            prison.GetComponent<Image>().sprite = FindIcon("PrisonPassive");
        }
        else
        {
            prison.GetComponent<Image>().sprite = FindIcon("PrisonActive");
        }
    }

    private Sprite FindIcon(string name)
    {
        foreach(Sprite obj in icons)
        {
            if (obj.name == name)
                return obj;
        }
        return null;
    }

    public void OnClickCatch(int index)
    {
        if (levelMover.isToogleBlocked)
            return;
        if(car.catchedRobbers == 3 || bannedButtons.Contains(index))
        {
            return;
            //change to grey
        }
        catching.StartCatch(index);
        ui.TurnOffGameplayUI();
    }

    public void OnClickPrison()
    {
        if (levelMover.isToogleBlocked)
            return;
        if (car.catchedRobbers == 0)
        {
            return;
            //change to grey
        }
        hints.GoPrison();
        catching.ToPRison();
        ui.TurnOffGameplayUI();
    }

    
}
