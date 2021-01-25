using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrica : MonoBehaviour, IInteractiveBuilding
{
    private LevelManager Level;
    private GameObject tourist;
    private GameObject rider;
    private GameObject car;
    private Animator playerAnim;
    [SerializeField]
    private GameObject[] IceCreamBoxesPrefabs;
    [SerializeField]
    private GameObject[] Markers;
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
    }
    IEnumerator EndInteractive()
    {
        playerAnim.Play("Walk");
        tourist.GetComponent<Character>().GoTo(car.transform.position);
        yield return new WaitWhile(() => tourist.GetComponent<Character>().isMoving);
        tourist.SetActive(false);
        rider.SetActive(true);
        LevelManager.isMoved = true;
        Unlock();
    }
    IEnumerator GenerateIceCream() 
    {
        for (int i = 3; i > 0; i-- ) 
        {
            GameObject obj = Instantiate(IceCreamBoxesPrefabs[0],canvas.transform);
            obj.transform.position = Markers[0].transform.position;
            obj.GetComponent<IceCreamBox>().MoveTo(Markers[i].transform.position);
            obj.GetComponent <IceCreamBox>().canvas = canvas;
            boxes.Add(obj);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitWhile(()=> IceBar.value!=3);
        StartCoroutine(EndInteractive());
    }

    void Unlock()
    {
        ClearBox();
        LevelManager.isMoved = true;
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
