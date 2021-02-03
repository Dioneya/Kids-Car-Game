using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedKids : MonoBehaviour, IInteractiveBuilding
{
    [SerializeField]
    private GameObject[] kids;
    [SerializeField]
    private GameObject[] Markers;
    private LevelManager Level;
    private GameObject iceCreamSaler;
    private GameObject rider;
    private GameObject car;
    private Animator playerAnim;
    private MissionProgress mission;

    private int cntHungryKids = 3;
    private int mustFeedKids = 3;
    private int cntFeededKids = 0;

    Dictionary<Settings.Difficult, int> param = new Dictionary<Settings.Difficult, int>()
    {
        { Settings.Difficult.Easy , 3 },
        { Settings.Difficult.Medium, 6},
        { Settings.Difficult.Hard, 9}
    };

    List<GameObject> boxes = new List<GameObject>();
    void Start()
    {
        Level = LevelManager.GetLevelManager();
        rider = Level.GetRider();
        car = Level.car;
        iceCreamSaler = Level.character.gameObject;
        playerAnim = iceCreamSaler.GetComponent<Animator>();
        mustFeedKids = param[Settings.difficult];
        mission = Level.gameObject.GetComponent<MissionProgress>();
    }

    void IInteractiveBuilding.Action()
    {
        Vector3 newPos = new Vector3(car.transform.position.x, iceCreamSaler.transform.position.y, iceCreamSaler.transform.position.z);
        iceCreamSaler.transform.position = newPos;
        iceCreamSaler.SetActive(true);
        rider.SetActive(false);
        StartCoroutine(WaitForCharStop(iceCreamSaler.GetComponent<Character>()));
    }

    IEnumerator WaitForCharStop(Character charact)
    {
        charact.GoTo(transform.position);

        yield return new WaitWhile(() => charact.isMoving);
        playerAnim.Play("Idle");
        StartCoroutine(GenerateKids());
    }

    IEnumerator GenerateKids()
    {
        iceCreamSaler.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        var generateKids = new List<GameObject>();
        foreach (GameObject i in kids)
            generateKids.Add(i);

        for (int i = 3; i > 0; i--)
        {
            int rnd = RandomVar(generateKids.Count);
            GameObject obj = Instantiate(generateKids[rnd], transform);
            obj.transform.position = Markers[0].transform.position;
            obj.GetComponent<kid>().MoveTo(Markers[i].transform.position);
            boxes.Add(obj);
            generateKids.RemoveAt(rnd);
            yield return new WaitForSeconds(2f);
        }
        IceCar.feedChild.Invoke();
        StartCoroutine(WaitForEndFeed());
    }
    public void KidFeeded() 
    {
        cntFeededKids++;
        cntHungryKids--;
    }
    IEnumerator WaitForEndFeed() 
    {
        yield return new WaitWhile(()=>cntHungryKids!=0);
        if (cntFeededKids != mustFeedKids)
        {
            cntHungryKids = 3;
            StartCoroutine(GenerateKids());
        }
        else 
        {
            StartCoroutine(EndInteractive());
        }    
    }

    IEnumerator EndInteractive()
    {
        IceCar.endFeed.Invoke();
        IceBar.removeValue.Invoke();
        playerAnim.Play("Walk");
        iceCreamSaler.GetComponent<Character>().GoTo(car.transform.position);
        yield return new WaitWhile(() => iceCreamSaler.GetComponent<Character>().isMoving);
        iceCreamSaler.SetActive(false);
        rider.SetActive(true);
        LevelManager.isMoved = true;
        Unlock();
    }

    void Unlock()
    {
        car.GetComponent<CarV2>().MoveToPrevPos();
        GameObject obj = Level.GetGameplayBtn();
        obj.SetActive(true);

        mission.TaskComplete(param[Settings.difficult]/3);
    }
    int RandomVar(int lenght)
    {
        return Random.Range(0, lenght);
    }
}
