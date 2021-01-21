using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeChildren : MonoBehaviour
{
    LevelMover levelMover;
    GameObject obj;

    private void Start()
    {
        levelMover = FindObjectOfType<LevelMover>();

        var r = Random.Range(0, transform.childCount);

        for(int i = 0;i<transform.childCount; i++)
        {
            if(i == r)
            {
                obj = transform.GetChild(i).gameObject;
                obj.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(levelMover.movingSpeed != 0)
        {
            obj.AddComponent<LevelPartMovement>();
            obj.transform.parent = null;
            enabled = false;
        }
    }
}
