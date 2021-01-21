using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsGenerator : MonoBehaviour
{
    [SerializeField]
    private CarParts carParts;
    public List<GameObject>  parts;
    public GameObject[] takedPlaces;
    public string audioPath;

    [SerializeField]private Transform spawnPoint;

    private void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.FindGameObjectWithTag("DetailGeneratorPoint").transform;
            spawnPoint.parent = null;
            transform.position = spawnPoint.position;
        }
        LoadAssets();
        PrepareAssets();
        takedPlaces = new GameObject[4] { null, null, null, null };
        GenerateNext(false);
    }

    private void LoadAssets()
    {
        parts = new List<GameObject>(carParts.CarDetails.GetCarDetails());
    }

    private void PrepareAssets()
    {
        foreach(GameObject child in parts)
        {
            if (child.GetComponent<Rigidbody2D>() == null)
                child.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            if (child.GetComponent<MovableObject>() == null)
                child.gameObject.AddComponent<MovableObject>();
            
            if (child.GetComponent<BoxCollider2D>() == null)
                child.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;

            if (child.GetComponent<PartSettings>() == null)
                child.gameObject.AddComponent<PartSettings>();
            child.GetComponent<PartSettings>().audioPath = audioPath;
        }
    }
    
    public GameObject GetRandomDetail()
    {
        int r = Random.Range(0,takedPlaces.Length);
        var c = 0;
        while(takedPlaces[r] == null)
        {
            c++;
            if (c > 20)
                break;

            r = Random.Range(0, takedPlaces.Length);
        }
        return takedPlaces[r];
    }
    public bool IsAllDetailsSetUp()
    {
        var counter = 0;
        if (parts.Count != 0)
            return false;
        for (int i = 0; i < takedPlaces.Length; i++)
        {
            if (takedPlaces[i].gameObject != null && takedPlaces[i].gameObject.activeSelf)
            {
                counter++;
            }
        }
        if (counter > 0)
            return false;
        return true;
    }
    public void GenerateAllFreeSlots()
    {
        for (int i = 0;i<takedPlaces.Length;i++)
        {
            if (takedPlaces[i] == null || takedPlaces[i].gameObject == null || !takedPlaces[i].gameObject.activeSelf)
            {
                GenerateNext(true);
            }
        }
    }
    public void GenerateNext(bool isRandom)
    {
        var partIndex = 0;
        if (isRandom)
        {
            partIndex = Random.Range(0, parts.Count);
        }
        if (parts.Count == 0)
        {
            return;
        }

        var obj = Instantiate(parts[partIndex], transform.position, Quaternion.identity);
        var p = obj.GetComponent<PartSettings>();

        obj.GetComponent<SpriteRenderer>().enabled = true;

        if (p != null)
        {
            obj.transform.localScale = p.smallSize;
            if (!p.takeAllPlace)
            {
                for (int i = 0; i < takedPlaces.Length; i++)
                {
                    if (takedPlaces[i] == null || !takedPlaces[i].gameObject.activeSelf)
                    {
                        obj.transform.localPosition += ConvertPlaceToPos(i);
                        takedPlaces[i] = obj;
                        parts.RemoveAt(partIndex);
                        return;
                    }
                }
            }
            else
            {
                takedPlaces[0] = obj;
            }
        }


        parts.RemoveAt(partIndex);
    }
    private Vector3 ConvertPlaceToPos(int index)
    {
        var x = 0.75f;
        var y = 1f;

        switch (index)
        {
            case 0:
                return new Vector3(-x,y,0);
            case 1:
                return new Vector3(x, y, 0);
            case 2:
                return new Vector3(-x, -y, 0);
            case 3:
                return new Vector3(x, -y, 0);
        }

        return Vector3.zero;
    }
}
