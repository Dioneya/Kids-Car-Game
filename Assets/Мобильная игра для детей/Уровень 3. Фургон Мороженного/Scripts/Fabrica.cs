using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrica : MonoBehaviour, IInteractiveBuilding
{
    [SerializeField]
    private GameObject[] IceCreamBoxesPrefabs;
    [SerializeField]
    private GameObject[] Markers;
    [SerializeField]
    private Animator conveyor;
    [SerializeField] private GameObject money;
    private LevelManager Level;
    private GameObject tourist;
    private GameObject rider;
    private GameObject car;
    private Animator playerAnim;
    private Canvas canvas;
    
    

    List<GameObject> boxes = new List<GameObject>();
    void Start()
    {
        Level = LevelManager.GetLevelManager();
        rider = Level.GetRider();
        car = Level.car;
        tourist = Level.character.gameObject;
        playerAnim = tourist.GetComponent<Animator>();
        canvas = transform.parent.transform.parent.GetComponent<Canvas>(); // /GeneratedObjects/Canvas
        IceBar.addValue.AddListener(CheckForEmpty);
    }
    void onDestroy() 
    {
        IceBar.addValue.RemoveListener(CheckForEmpty);
    }
    void IInteractiveBuilding.Action()
    {
        Vector3 newPos = new Vector3(car.transform.position.x, tourist.transform.position.y, tourist.transform.position.z);
        tourist.transform.position = newPos;
        tourist.SetActive(true);
        rider.SetActive(false);
        StartCoroutine(WaitForCharStop(tourist.GetComponent<Character>()));
    }
    IEnumerator WaitForCharStop(Character charact)
    {
        charact.GoTo(Markers[0].transform.position);

        yield return new WaitWhile(() => charact.isMoving);
        playerAnim.Play("Idle");
        StartCoroutine(GenerateIceCream());
        StartCoroutine(EndInteractive());
    }
    IEnumerator EndInteractive()
    {
        yield return new WaitWhile(() => IceBar.value != 3);
        StartCoroutine(MoneyTransform());
        playerAnim.Play("Walk");
        tourist.GetComponent<Character>().GoTo(car.transform.position);
        yield return new WaitWhile(() => tourist.GetComponent<Character>().isMoving);
        tourist.SetActive(false);
        rider.SetActive(true);
        LevelManager.isMoved = true;
        Unlock();
    }

    void CheckForEmpty() 
    {
        int cnt = 0;
        foreach (GameObject i in boxes)
            if (!i.activeSelf)
                cnt++;
        if (cnt >= 2) 
        {
            ClearBox();
            StartCoroutine(GenerateIceCream());
        }
            
    }
    IEnumerator GenerateIceCream() 
    {
        var generateBox = new List<GameObject>();
        foreach (GameObject i in IceCreamBoxesPrefabs)
            generateBox.Add(i);

        conveyor.Play("Work");
        for (int i = 3; i > 0; i-- ) 
        {
            int rnd = RandomVar(generateBox.Count);  
            GameObject obj = Instantiate(generateBox[rnd],canvas.transform);
            obj.transform.position = Markers[0].transform.position;
            obj.GetComponent<IceCreamBox>().MoveTo(Markers[i].transform.position);
            obj.GetComponent <IceCreamBox>().canvas = canvas;
            boxes.Add(obj);

            generateBox.RemoveAt(rnd);
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(1f);
        conveyor.Play("Default");
    }

    int RandomVar(int lenght) 
    {
        return Random.Range(0,lenght);
    }
    IEnumerator MoneyTransform()
    {
        money.SetActive(true);
        Vector3 currentStart = money.transform.localPosition;
        Vector3 endPos = currentStart;
        endPos.y = 2f;

        for (float i = 0; i <= 1f; i += 0.02f)
        {
            yield return new WaitForSeconds(0.02f);
            money.transform.localPosition = Vector3.Lerp(currentStart, endPos, i);
        }

        money.SetActive(false);
    }

    void Unlock()
    {
        ClearBox();
        LevelManager.isMoved = true;
        car.GetComponent<CarV2>().MoveToPrevPos();
        GameObject obj = Level.GetGameplayBtn();
        obj.SetActive(true);
    }

    void ClearBox() 
    {
        for (int i = 0; i < boxes.Count; i++)
            Destroy(boxes[i]);
        boxes.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
