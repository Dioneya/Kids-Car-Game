using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBuldingStuff : MonoBehaviour
{
    public List<Stuff> stuff;

    private void Start()
    {
        RandomizeBranch();
    }

    private void RandomizeBranch()
    {
        foreach (Stuff s in stuff)
        {
            var r = UnityEngine.Random.Range(0f, 1f);
            if (s.rate <= r)
            {
                for (int i =0;i<transform.childCount;i++)
                {
                    if (transform.GetChild(i).name.Equals(s.name))
                    {
                        var o = transform.GetChild(i).gameObject;
                        o.SetActive(true);
                        o.transform.parent = null;
                        o.AddComponent<LevelPartMovement>().levelMover = GetComponent<LevelPartMovement>().levelMover;
                        if (s.isOnlyOneFromAll) return;
                    }
                }
            }
        }
    }

}

[Serializable]
public class Stuff
{
    public string name;
    public float rate;
    public bool isOnlyOneFromAll;
}
