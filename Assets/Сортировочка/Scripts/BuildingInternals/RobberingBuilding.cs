using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberingBuilding : MonoBehaviour, IInteractiveBuilding
{
    

    public void Action()
    {
        gameObject.AddComponent<RobbersEnableOnBuilding>();
        InstantiateLight();
    }

    public void InstantiateLight()
    {
        var c = FindObjectOfType<CatchingRobbers>();
        if (c.light == null)
            c.light = Instantiate(c.lightPrefab, new Vector3(2.252747f, -2.34066f), Quaternion.Euler(new Vector3(0, 0, 35)));
        else
            c.light.transform.position = new Vector3(2.252747f, -2.34066f);
            c.light.SetActive(true);
    }
}
