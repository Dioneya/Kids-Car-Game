using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonBuilding : MonoBehaviour, IInteractiveBuilding
{
    [SerializeField] private Prison prisonBuilding;
    public void Action()
    {
        InstantiateRobbers();
    }
    public void InstantiateRobbers()
    {
        var policeman = FindObjectOfType<Level>().character.gameObject;
        policeman.SetActive(true);
        policeman.GetComponent<CharacterMovement>().Stop();
        policeman.GetComponent<CharacterAnimations>().Idle();
        var carchingRobbers = FindObjectOfType<CatchingRobbers>();
        carchingRobbers.robbers = new List<GameObject>();
        var i = 0;
        prisonBuilding.ClearRobberList();
        foreach (GameObject o in carchingRobbers.robbersPrefab)
        {
            foreach (CatchingRobbers.ColorType color in carchingRobbers.catchedRobbers)
            {
                Debug.Log(color.ToString() + " | " + o.name);
                if (color.ToString() == o.name)
                {
                    var robber = Instantiate(o, new Vector3(1.5f + i * 1.7f, -3f, 0), Quaternion.identity) ;
                    i++;
                    carchingRobbers.robbers.Add(robber);
                    robber.AddComponent<MovableObject>();
                    prisonBuilding.AddRobber(robber.AddComponent<RobberWindowInteraction>().InitColor(color.ToString()));
                    robber.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    var settings = robber.gameObject.AddComponent<PartSettings>();
                    settings.destionationObjectRangeInstall = 0.7f;
                    settings.isColorMatching = true;
                }
            }

        }
        policeman.transform.position = new Vector3(6,-2.7f,0);
        carchingRobbers.catchedRobbers.Clear();
        carchingRobbers.car.LeaveCarAll();
        carchingRobbers.car.ClearStars();
        prisonBuilding.Init();
    }
}
