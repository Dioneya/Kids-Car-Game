using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject road;
    private float passedDistanceFromBackCity;
    private float passedDistanceFromBackBuilding;
    private float passedDistanceFromBuilding;
    private float passedDIstanceFromCloud;
    private float nextRoadStuffAfter = 0;
    [SerializeField] private LevelEnvironment _levelEnvironment;
    [SerializeField] private GameObject _lightGameObject;
    private List<GameObject> backCity;
    private List<GameObject> backBuildings;
    private List<GameObject> backTrees;
    private List<GameObject> uniq;
    private List<GameObject> simple;
    private List<GameObject> trees;
    private List<GameObject> bushes;
    private List<GameObject> fence;
    private List<GameObject> branches;
    private List<GameObject> coreBuilding;
    private List<GameObject> clouds;
    private List<GameObject> roadStuff;
    private Vector3 roadTop; 
    private int nextBuildingWillSpawn;
    private int nextUniqWillSpawn;
    private int nextBackBuildingWillSpawn;
    private int nextRoadStuffWillSpawn;
    [SerializeField]
    private LevelMover _levelMover;
    [SerializeField]
    private RandomizeBuldingStuff settingsSimpleBuildings;
    private bool spawNextUniq = false;
    private bool spawNextFakeUniq = false;
    private GameObject fakeUniq;
    public GameObject LightGameObject { get => _lightGameObject; set => _lightGameObject = value; }
    [SerializeField] private Vector3 _initPosition;

    void Awake()
    {
        roadTop = road.transform.position + Vector3.up * road.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
    }
    private void Start()
    {
        LoadEnvironment();
    }
    private void LoadEnvironment()
    {
        backCity = new List<GameObject>(_levelEnvironment.BackCity);
        backBuildings = new List<GameObject>(_levelEnvironment.BackBuildings);
        backTrees = new List<GameObject>(_levelEnvironment.BackTrees);
        uniq = new List<GameObject>(_levelEnvironment.Uniq);
        simple = new List<GameObject>(_levelEnvironment.Simple);
        trees = new List<GameObject>(_levelEnvironment.Trees);
        bushes = new List<GameObject>(_levelEnvironment.Bushes);
        fence = new List<GameObject>(_levelEnvironment.Fence);
        branches = new List<GameObject>(_levelEnvironment.Branches);
        coreBuilding = new List<GameObject>(_levelEnvironment.CoreBuilding);
        clouds = new List<GameObject>(_levelEnvironment.Clouds);
        roadStuff = new List<GameObject>(_levelEnvironment.RoadStuff);
       // fakeUniq =  _levelEnvironment.FakeUniq;
        AddComponentsToAll();
        SpawnStartEnvironment();
        _levelMover.GetComponent<LevelSpeed>().Stop();
        enabled = false;
    }

    private void AddComponentsToAll()
    {
        AddComponents(backCity);
        AddComponents(backBuildings);
        AddComponents(backTrees);
        AddComponents(uniq);
        AddComponents(simple);
        AddComponents(trees);
        AddComponents(bushes);
        AddComponents(fence);
        AddComponents(branches);
        AddComponents(coreBuilding);
        AddComponents(clouds);
        AddComponents(roadStuff);
    }
    private void AddComponents(List<GameObject> arr)
    {
        foreach(GameObject obj in arr)
        {
            var rb = obj.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody2D>();
            }
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }

    void FixedUpdate()
    {
        if (passedDistanceFromBackBuilding <= 0)
        {
          SpawnBackBuilding();
        }

        if (passedDistanceFromBuilding <= 0)
        {
            if (spawNextFakeUniq)
            {
                var offset = fakeUniq.GetComponent<SpriteRenderer>().bounds.size.x * 1.5f - 2;
                var uniq = Instantiate(fakeUniq, transform.position + Vector3.right * offset, Quaternion.identity);
                var mover = uniq.gameObject.AddComponent<LevelPartMovement>();
                mover.levelMover = _levelMover;
                uniq.AddComponent<CarStopper>().levelMover = _levelMover;
                mover.parallaxCoef = 1;
                mover.end = new Vector3(-100,mover.end.y, mover.end.z);
                var spriteU = uniq.GetComponent<SpriteRenderer>();
                passedDistanceFromBuilding = offset * 2 +6;
                spawNextFakeUniq = false;
            }
            else
                SpawnFrontBuilding();
        }
        passedDistanceFromBackBuilding -= _levelMover.movingSpeed * _levelMover.ParallaxCoef("BackBuildings") * Time.deltaTime;
        passedDistanceFromBuilding -= _levelMover.movingSpeed  * Time.deltaTime;

    }
    private void SpawnStartEnvironment()
    {
        //var initPos = new Vector3(-14.5f, 4.0f, 0);
        var city = Instantiate(backCity[0], Vector3.zero, Quaternion.identity);
        city.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
        var core = Instantiate(coreBuilding[0], coreBuilding[0].transform.position, Quaternion.identity);
        core.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;

    }
    private void SpawnFrontBuilding()
    {

        for (int i = 0; i < 4; i++)
        {
            var sim = Instantiate(simple[nextBuildingWillSpawn], transform.position + Vector3.right * passedDistanceFromBuilding, Quaternion.identity);

            sim.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
            sim.gameObject.AddComponent<RandomizeBuldingStuff>().stuff = settingsSimpleBuildings.stuff;
            var spriteC = sim.GetComponent<SpriteRenderer>();
            passedDistanceFromBuilding += spriteC.sprite.bounds.size.x / 2 + 0.3f;

            var betweenPos = transform.position + Vector3.right * passedDistanceFromBuilding;

            var tree = Instantiate(trees[Random.Range(0,trees.Count)],betweenPos,Quaternion.identity);
            var bush = Instantiate(bushes[Random.Range(0, bushes.Count)], betweenPos, Quaternion.identity);
            var fen = Instantiate(fence[Random.Range(0, fence.Count)], betweenPos, Quaternion.identity);

            PutOnRoad(sim);
            PutOnRoad(tree);
            PutOnRoad(bush);
            PutOnRoad(fen);

            tree.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
            bush.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
            fen.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;

            if(nextRoadStuffAfter == 0)
            {
                var stuff = Instantiate(roadStuff[nextRoadStuffWillSpawn], betweenPos, Quaternion.identity);
                PutOnRoad(stuff);
                stuff.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
                nextRoadStuffAfter = 2;
            }
            nextRoadStuffAfter--;

            nextRoadStuffWillSpawn = RandomExcept(0, roadStuff.Count, nextRoadStuffWillSpawn);
            nextBuildingWillSpawn = RandomExcept(0, simple.Count, nextBuildingWillSpawn);
            if(i<3)
                passedDistanceFromBuilding += simple[nextBuildingWillSpawn].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + 0.3f;
            else if(i == 3)
            {
                passedDistanceFromBuilding += uniq[nextUniqWillSpawn].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + 0.3f;
            }
        }
        var uniqB = Instantiate(uniq[nextUniqWillSpawn], transform.position + Vector3.right * passedDistanceFromBuilding, Quaternion.identity);
        if (spawNextUniq)
        {
            
            uniqB.gameObject.AddComponent<CarStopper>().levelMover = _levelMover;
            spawNextUniq = false;
        }
        uniqB.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
        var spriteU = uniqB.GetComponent<SpriteRenderer>();
        passedDistanceFromBuilding += spriteU.sprite.bounds.size.x / 2 + 0.3f;
        var betweenPosU = transform.position + Vector3.right * passedDistanceFromBuilding;
        var treeU = Instantiate(trees[Random.Range(0, trees.Count)], betweenPosU, Quaternion.identity);
        var bushU = Instantiate(bushes[Random.Range(0, bushes.Count)], betweenPosU, Quaternion.identity);
        var fenU = Instantiate(fence[Random.Range(0, fence.Count)], betweenPosU, Quaternion.identity);

        PutOnRoad(uniqB);
        PutOnRoad(treeU);
        PutOnRoad(bushU);
        PutOnRoad(fenU);

        treeU.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
        bushU.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
        fenU.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;

        if (nextRoadStuffAfter == 0)
        {
            var stuff = Instantiate(roadStuff[nextRoadStuffWillSpawn], betweenPosU, Quaternion.identity);
            PutOnRoad(stuff);
            stuff.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
            nextRoadStuffAfter = 2;
        }
        nextRoadStuffAfter--;
        nextUniqWillSpawn = RandomExcept(0, uniq.Count, nextUniqWillSpawn);
        passedDistanceFromBuilding += simple[nextBuildingWillSpawn].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + 0.3f;
    }

    private void SpawnBackBuilding()
    {
        var back = Instantiate(backBuildings[nextBackBuildingWillSpawn], transform.position, Quaternion.identity);
        back.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
        var tree = Instantiate(backTrees[Random.Range(0, backTrees.Count)], transform.position + Vector3.down +
                               Vector3.right * (back.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + 0.3f), Quaternion.identity);
        tree.gameObject.AddComponent<LevelPartMovement>().levelMover = _levelMover;
        nextBackBuildingWillSpawn = RandomExcept(0, backBuildings.Count, nextBackBuildingWillSpawn);
        passedDistanceFromBackBuilding = back.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 +
                                         backBuildings[nextBackBuildingWillSpawn].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 + 0.6f;
    }
    public void SpawnExceptUniq(string[] except)
    {
        spawNextUniq = true;
        nextUniqWillSpawn = RandomExceptName(0,uniq.Count, nextUniqWillSpawn,except,uniq);
    }

    public void SpawnConcreteUniq(string building)
    {
        spawNextUniq = true;
        for (int i = 0;i<uniq.Count; i++)
        {
            if (uniq[i].name == building)
            {
                nextUniqWillSpawn = i;
            }
        }
    }

    public void SpawnAsUniq(string path)
    {
        spawNextFakeUniq = true;
        fakeUniq = (Resources.Load<GameObject>(path));
    }

    private void PutOnRoad(GameObject obj)
    {
        var x = obj.transform.position.x;
        var y = roadTop.y + obj.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        obj.transform.position = new Vector3(x, y - 0.1f, 0);
    }


    public int RandomExcept(int min,int max,int except)
    {
        var res = except;
        while (res == except)
        {
            res = Random.Range(min,max);
        }

        return res;
    }

    private int RandomExceptName(int min, int max, int except, string[] exceptArr,List<GameObject> arr)
    {
        List<int> alsoExcept = new List<int>();

        for(int i = 0; i < arr.Count; i++)
        {
            for (int j = 0; j < exceptArr.Length; j++)
            {
                if (arr[i].name == exceptArr[j])
                {
                    alsoExcept.Add(i);
                }
            }
        }
        var res = except;
        while (res == except || alsoExcept.Contains(res))
        {
            res = Random.Range(min, max);
        }
        return res;
    }
}
