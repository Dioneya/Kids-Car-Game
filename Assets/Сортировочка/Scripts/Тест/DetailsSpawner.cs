using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DetailsSpawner : MonoBehaviour
{

    [SerializeField] private CarV2 car;
    public List<GameObject> details = new List<GameObject>();
    [SerializeField] private List<Vector3> spawnCoord = new List<Vector3> {
                                                          new Vector3(437f, 203f, 0f),
                                                          new Vector3(641f, 203f, 0f),
                                                          new Vector3(437f, -35f, 0f),
                                                          new Vector3(641f, -35f, 0f),
                                                          new Vector3(542f, 86f, 0f)
                                                         };
    public HandTips hand;
    bool isAfterFirst = false;
    bool isFirst = true;
    void Start()
    {
        Generate(spawnCoord[4]);
        isFirst = false;
        isAfterFirst = true;
    }
    public void Generate(Vector3 position)
    {
        if (isAfterFirst)
        {
            AfterKuzovGenerate();
            isAfterFirst = false;
        }
        else 
        {
            if (details.Count > 0)
            {
                var rnd = new System.Random();
                int indx = isFirst ? 0 : rnd.Next(0, details.Count - 1);

                GameObject detail = Instantiate(details[indx], gameObject.transform);

                SettingObj(ref detail, position);
                details.Remove(details[indx]);
            }
        }
        
    }

    private void AfterKuzovGenerate()
    {
        for (int i = 0; i < 4; i++)
        {
            var rnd = new System.Random();
            int indx = isFirst ? 0 : rnd.Next(0, details.Count - 1);

            GameObject detail = Instantiate(details[indx], gameObject.transform);

            SettingObj(ref detail, spawnCoord[i]);
            details.Remove(details[indx]);
        }
    }
    private void SettingObj(ref GameObject detail, Vector3 position) 
    {
        DragAndDropV2 dnd = detail.GetComponent<DragAndDropV2>();
        Detail detail1 = detail.GetComponent<Detail>();

        dnd.car = car;
        detail.transform.localPosition = position;
        Debug.Log(detail.transform.localPosition + " " + detail.transform.position);
        dnd.canvas = gameObject.transform.parent.gameObject.GetComponent<Canvas>();
        detail1.spawnLocalPosition = position;
        detail1.spawnPosition = detail.transform.position;
    }
}
