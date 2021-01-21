using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prison : MonoBehaviour
{
    [SerializeField] GameObject placesPool;
    PoliceLevel policeLevel;
    List<GameObject> places;
    [SerializeField]
    private List<RobberWindowInteraction> _robbers = new List<RobberWindowInteraction>();
    [SerializeField]
    private List<int> indexes = new List<int>();
    public void AddRobber(RobberWindowInteraction robber)
    {
        _robbers.Add(robber);
    }
    public void ClearRobberList()
    {
        _robbers.Clear();
    }
    private void Start()
    {
        policeLevel = FindObjectOfType<PoliceLevel>();
    }

    public void Init()
    {
        for(int i = 0; i < placesPool.transform.childCount; i++)
        {
            if (policeLevel.windowPlace.Contains(i))
            {
                placesPool.transform.GetChild(i).tag = i.ToString();
                var ind = policeLevel.windowPlace.IndexOf(i);
                var place = placesPool.transform.GetChild(i);
                // это позиция окна 
                indexes.Add(i);

                var sr = place.gameObject.AddComponent<SpriteRenderer>();
                var color = policeLevel.windowColor[ind];
                print(color);
                foreach (var item in _robbers)
                {
                    print(_robbers.Count);
                    print(item.ColorName + "|||||" + color.ToString());
                    if (item.ColorName == color.ToString() && policeLevel.windowState[ind])
                    {
                        item.InitTarget(placesPool.transform.GetChild(i).gameObject);
                    }
                }
                var state = policeLevel.windowState[ind];
                Sprite[] arr = new Sprite[1];
                if (state)
                {
                    arr = policeLevel.emptyWindows;
                }
                else
                {
                    arr = policeLevel.filledWindows;
                }
                var index = FindColorIndex(arr, color);
                sr.sprite = arr[index];
            }
        }
    }

    
    public void FillWindow(GameObject obj)
    {
        var i = policeLevel.windowPlace.IndexOf(int.Parse(obj.name));
        var index = FindColorIndex(policeLevel.filledWindows, policeLevel.windowColor[i]);
        obj.GetComponent<SpriteRenderer>().sprite = policeLevel.filledWindows[index];
        policeLevel.windowState[i] = false;
    }

    private int FindColorIndex(Sprite[] arr,CatchingRobbers.ColorType color)
    {
        for(int i =0;i< arr.Length; i++)
        {
            if(arr[i].name == color.ToString())
            {
                return i;
            }
        }
        return 0;
    }
}
