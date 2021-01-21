using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbersEnableOnBuilding : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    private CatchingRobbers catchingRobbers;
    CatchingRobbers.ColorType currentColorType;
    private PolicemanOutsideCar policemanOutsidCar;

    public void Start()
    {
        policemanOutsidCar = FindObjectOfType<PolicemanOutsideCar>();
        policemanOutsidCar.CreatePoliceman();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        catchingRobbers = levelGenerator.GetComponent<CatchingRobbers>();
        currentColorType = catchingRobbers.types[catchingRobbers.currentTypeCatching];

        Transform robbers = null; 
        for (int i = 0; i < transform.childCount; i++)
        {
            if("Robbers" == transform.GetChild(i).name)
            {
                robbers = transform.GetChild(i);
            }
        }

        for (int i = 0; i < robbers.childCount; i++)
        {
            if (currentColorType.ToString() == robbers.GetChild(i).name)
            {
                var color = robbers.GetChild(i);
                var r = Random.Range(0, color.childCount);
                

                for (int j = 0; j < color.childCount; j++)
                {

                    if(j == r)
                    {
                        var robber = color.GetChild(j);
                       // robber.gameObject.AddComponent<BoxCollider2D>();
                        color.gameObject.SetActive(true);
                        robber.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                        robber.gameObject.AddComponent<MovableObject>().IsCanDrag=false;
                        robber.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                        robber.gameObject.AddComponent<RobberCarInteraction>();
                        robber.gameObject.GetComponent<Robber>().FollowOnEvent();
                        robber.gameObject.GetComponent<Robber>().DropState = new RobberCatchDropState();
                        var sound = robber.gameObject.GetComponent<FillingRobber>();
                        sound.RobbersSound.PlayTimerAudio(false);
                        var settings = robber.gameObject.AddComponent<PartSettings>();
                        settings.destionationObjectRangeInstall = 3.5f;
                        settings.destinationObjectTag = "CarBase";

                    }
                    else
                    {
                        color.GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
        }

    }
}
