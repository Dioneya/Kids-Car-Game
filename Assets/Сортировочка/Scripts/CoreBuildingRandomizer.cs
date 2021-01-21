using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBuildingRandomizer : MonoBehaviour
{
    [SerializeField] List<GameObject> buildings;
    [SerializeField] GameObject back;
    List<GameObject> objects;
    LevelMover levelMover;

    private void Start()
    {

        levelMover = FindObjectOfType<LevelMover>();
        FrontSpawn();
        BackSpawn();
    }

    private void BackSpawn()
    {
        for (int i = 0; i < back.transform.childCount; i++)
        {
            var child = back.transform.GetChild(i);
            var r = Random.Range(0, child.childCount);
            for (int j = 0; j < child.childCount; j++)
            {
                if (r == j)
                {
                    var backBuilding = child.GetChild(j);
                    backBuilding.gameObject.SetActive(true);
                    objects.Add(backBuilding.gameObject);
                }
                //backBuilding.gameObject.AddComponent<LevelPartMovement>().parallaxCoef = -1;
                //backBuilding.parent = null;
            }
        }
    }

    private void FrontSpawn()
    {
        var r = -1;
        objects = new List<GameObject>();

        foreach (GameObject b in buildings)
        {
            r = RandomExcept(0, b.transform.childCount, r);
            var building = b.transform.GetChild(r).gameObject;
            building.SetActive(true);
            objects.Add(building);
        }
    }

    public int RandomExcept(int min, int max, int except)
    {
        var res = except;
        while (res == except)
        {
            res = Random.Range(min, max);
        }

        return res;
    }

    private void Update()
    {
        if (levelMover.movingSpeed != 0)
        {
            foreach (GameObject obj in objects)
            {
                obj.AddComponent<LevelPartMovement>();
                obj.transform.parent = null;
            }
                
            enabled = false;
        }
    }
}
