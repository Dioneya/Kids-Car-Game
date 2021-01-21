using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParts : MonoBehaviour , ICarParts
{
    [SerializeField] private CarDetails _carDetails;
    public CarDetails CarDetails => _carDetails;
    public IInteractiveAction[] interactiveComponents;
    public WinkerAction winker;

    [SerializeField] SpriteRenderer frontWindow;
    [SerializeField] SpriteRenderer backWindow;
    [SerializeField] List<Sprite> backWindowStates;
    [SerializeField] List<Sprite> frontWindowStates;
    [SerializeField] private bool isSoloDriver;
    [SerializeField] private SpriteRenderer _characterInCar;
    [SerializeField]
    private CarBorders borders;
    [SerializeField]
    private GameObject carBase;
    [SerializeField]
    private LevelMover levelMover;

    void Start()
    {
        winker = GetComponentInChildren<WinkerAction>();
        //carBase = GameObject.FindGameObjectWithTag("CarBase");
        //borders = FindObjectOfType<CarBorders>();
        PrepareAllPartsForStart();
        FindInteractives();
        levelMover = FindObjectOfType<LevelMover>();
        levelMover.OnNight += TurnLight;
        levelMover.OnDay += TurnOffLight;
    }

    //back window states: empty,one,two
    //front window states: empty,just robber,just policeman,both
    public void UpdateWindows(int robbers,bool isDriverIn)
    {
        if (!isSoloDriver)
        {
            if (robbers > 3)
            {
                Debug.Log("Robbers should be less than 4");
            }
            var back = robbers;
            var front = 0;

            if (robbers == 3)
            {
                back = 2;
                front = 1;
            }

            if (isDriverIn)
            {
                front += 2;
            }

            frontWindow.sprite = frontWindowStates[front];
            backWindow.sprite = backWindowStates[back];
        }
        else
        {
            if (isDriverIn)
            {
                _characterInCar.enabled = true;
            }
            else
            {
                _characterInCar.enabled = false;
            }
        }
    }

    private void FindInteractives()
    {
        interactiveComponents = GetComponentsInChildren<IInteractiveAction>();
    }

    private void TurnLight()
    {
        TurnOn(new LightAction());
    }

    private void TurnOffLight()
    {
        TurnOff(new LightAction());
    }

    public void TurnOn(IInteractiveAction type)
    {
        foreach (IInteractiveAction interactive in interactiveComponents)
        {
            if(interactive.GetType() == type.GetType())
            {
                //interactive.TurnOn();
            }
        }
    }

    public void TurnOff(IInteractiveAction type)
    {
        foreach (IInteractiveAction interactive in interactiveComponents)
        {
            if (interactive.GetType() == type.GetType())
            {
                //interactive.TurnOff();
            }
        }
    }

    public void TurnAll()
    {
        foreach (IInteractiveAction interactive in interactiveComponents)
        {
            interactive.TurnOn();
        }
    }

    public void TurnOffAll()
    {
        foreach (IInteractiveAction interactive in interactiveComponents)
        {
            interactive.TurnOff();
        }
    }

    private void PrepareAllPartsForStart()
    {
        for (int i = 0;i<transform.childCount;i++)
        {
            var child = transform.GetChild(i);
            child.GetComponent<SpriteRenderer>().enabled = false;
            //child.gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
    public bool IsInCarBase(Vector3 pos)
    {
        var sprite = carBase.GetComponent<SpriteRenderer>().sprite;
        var X = sprite.bounds.size.x;
        var Y = sprite.bounds.size.y;
        var res = false;

        if(carBase.transform.position.y - Y/2 < pos.y && carBase.transform.position.y + Y/2 > pos.y &&
            carBase.transform.position.x - X / 2 < pos.x && carBase.transform.position.x + X / 2 > pos.x)
        {
            res = true;
        }
        return res;
    }

    public bool TryToSetUpDetail(GameObject detail,float distanceToSetUp)
    {
        var checkSprite = detail.GetComponent<SpriteRenderer>().sprite;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if(child.GetComponent<SpriteRenderer>().sprite == checkSprite && Vector2.Distance(child.position,detail.transform.position) <= distanceToSetUp)
            {
                var spriteRend = child.GetComponent<SpriteRenderer>();
                if (spriteRend.enabled == true)
                    return false;

                spriteRend.enabled = true;
                child.GetComponent<IInteractiveAction>()?.Action();
                detail.GetComponent<MovableObject>().FinishMoving();
                borders.DisableChildObject(child.GetComponent<CarBorderDisable>());
                return true;
            }
        }
        return false;
    }
    public GameObject FindCarPartByDetail(GameObject detail)
    {
        var checkSprite = detail.GetComponent<SpriteRenderer>().sprite;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.GetComponent<SpriteRenderer>().sprite == checkSprite)
            {
                return child.gameObject;
            }
        }

        Debug.Log("No such car part or detal");
        return null;
    }
}
