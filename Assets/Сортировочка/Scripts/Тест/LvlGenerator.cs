using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
public class LvlGenerator : MonoBehaviour
{
    public enum Type { BackCity, Clouds, BackBuildings, Buildings, Road, Interactive, AZS };

    [Header("параметры")]
    public bool isInteractive = false;
    public bool SpawnInteractiveOnStart = true;
    private string nameOfInteractive;
    public Type InteractSpawnCanvas;

    private Dictionary<Type, List<GameObject>> GeneratedObjects = new Dictionary<Type, List<GameObject>>();
    [Header("Префабы для генератора")]
    [SerializeField] 
    private List<GameObject> BackCity = new List<GameObject>();
    [SerializeField] 
    private List<GameObject> Clouds = new List<GameObject>();
    [SerializeField] 
    private List<GameObject> BackBuildings = new List<GameObject>();
    [SerializeField] 
    private List<GameObject> Buildings = new List<GameObject>();
    [SerializeField]
    private List<GameObject> Roads = new List<GameObject>();


    [SerializeField] 
    private List<GameObject> Interactive = new List<GameObject>();
    [SerializeField] 
    private List<GameObject> CanvasList = new List<GameObject>();

    [SerializeField] private int CntOfSpawnableInteractive;
    

    [Header("Кастомные координаты для спауна")]
    [SerializeField]
    List<Vector3> CustomeCoord = new List<Vector3>
    {
        new Vector3(2793f, -138f, 0f),
        new Vector3(2005f, 384f, 0f), 
        new Vector3(2631f, -85f, 0f), 
        new Vector3(1976f, 0f, 0f), 
        new Vector3(3965f, -613f, 0f),
        new Vector3(3445f, -53f, 0f)
    };

    private Dictionary<Type, float[]> speeds = new Dictionary<Type, float[]>
    {
        { Type.BackCity,       new float[3] { 250f, 650f, 1050f } }, 
        { Type.Clouds,         new float[3] { 300f, 700f, 1100f } }, 
        { Type.BackBuildings,  new float[3] { 400f, 800f, 1200f } }, 
        { Type.Buildings,      new float[3] { 400f, 800f, 1200f } }, 
        { Type.Road,           new float[3] { 400f, 800f, 1200f } },
        { Type.Interactive,    new float[3] { 400f, 800f, 1200f } }
    };

    private Dictionary<Type, Vector3> spawnCoords = new  Dictionary<Type, Vector3> 
    {
         {Type.BackCity,       new Vector3(2793f, -138f, 0f)},
         {Type.Clouds,         new Vector3(2005f, 384f, 0f)}, 
         {Type.BackBuildings,  new Vector3(2631f, -85f, 0f)}, 
         {Type.Buildings,      new Vector3(1976f, 0f, 0f)}, 
         {Type.Road,           new Vector3(3965f, -613f, 0f)},
         {Type.Interactive,    new Vector3(3445f, -53f, 0f)}
    };

    private int lastIndxOfInteractive = -1;
    private Vector3 lastPosSpawnInteractive;
    
    void Start() 
    {
        for (int i = 0; i < spawnCoords.Keys.Count; i++)
            spawnCoords[(Type)i] = CustomeCoord[i]; 
                
        GeneratedObjects = new Dictionary<Type, List<GameObject>>()
        {
            { Type.BackCity, BackCity },
            { Type.Clouds, Clouds },
            { Type.BackBuildings, BackBuildings },
            { Type.Buildings, Buildings },
            { Type.Road, Roads },
            { Type.Interactive, Interactive }
        };

        if(SpawnInteractiveOnStart) 
            Generate(Type.Interactive);
    }

    public void Generate(Type type)
    {
        if (type == Type.Interactive && isInteractive)
        {
            GenerateInteractive(nameOfInteractive);
            isInteractive = false;
            CanvasList[(int)type].transform.GetChild(0).localPosition = lastPosSpawnInteractive;
        }
        else
        {
            GameObject obj = TakeObjFromList(GeneratedObjects[type], CanvasList[(int)type], type);
            obj.transform.localPosition = spawnCoords[type];
            SetConfigToObj(ref obj, speeds[type], CanvasList[(int)type], type);
        }
    }
    //Генерация кастомной дороги под интерактив
    public void GenerateRoadInteractive(GameObject interactive, GameObject road) 
    {
        GameObject obj = Instantiate(road, CanvasList[(int)Type.Interactive].transform.GetChild(2).gameObject.transform);
        obj.transform.localPosition = spawnCoords[Type.Road];
        obj.transform.SetAsFirstSibling();
        obj.transform.localPosition = new Vector3(interactive.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z);
        var move = obj.AddComponent<MoveEnviroment>();
        if (speeds[Type.Road].Length == 3)
            move.SetSpeed(speeds[Type.Road][0], speeds[Type.Road][1], speeds[Type.Road][2]);

        move.SetInfo(this, Type.Interactive, CanvasList[(int)Type.Interactive].transform.GetChild(0).gameObject, CanvasList[(int)Type.Interactive].transform.GetChild(1).gameObject);
        move.spawn = false;
    }
    //Установить настройки для MoveEnviroment
    private void SetConfigToObj(ref GameObject obj, float[] speed, GameObject canvas, Type type) 
    {
        var move = obj.AddComponent<MoveEnviroment>();
        if(speed.Length==3)
            move.SetSpeed(speed[0], speed[1], speed[2]);
        move.SetInfo(this, type, canvas.transform.GetChild(0).gameObject, canvas.transform.GetChild(1).gameObject);
        if (type == Type.Clouds)
            move.isAlwaysMove = true;
    }
    //Взять рандомный созданный объект из листа префабов
    private GameObject TakeObjFromList(List<GameObject> list, GameObject canvas, Type type) 
    {
        System.Random rnd = new System.Random();
        int index = lastIndxOfInteractive;
        
        if (type == Type.Interactive) // для избавление от одинакового спауна подряд интерактива
        {
            while (index == lastIndxOfInteractive)
            {
                index = rnd.Next(CntOfSpawnableInteractive);
            }
            lastIndxOfInteractive = index;
        }
        else
            index = rnd.Next(list.Count);

        return Instantiate(list[index], canvas.transform.GetChild(2).gameObject.transform);
    }

    public void AddInteractiveToQueue(string nameInteractive) 
    {
        isInteractive = true;
        nameOfInteractive = nameInteractive;
        lastPosSpawnInteractive = CanvasList[(int)Type.Interactive].transform.GetChild(0).localPosition;
        CanvasList[(int)Type.Interactive].transform.GetChild(0).localPosition = new Vector3(-2000f, 0 , 0);

    }

    public void GenerateInteractive(string nameInteractive) 
    {
        GameObject refObj = null;
        foreach (GameObject obj in Interactive)
        {
            if (obj.name == nameInteractive) 
            {
                refObj = obj;
                break;
            }
        }
        if (refObj != null)
            StartCoroutine(GenerateInteractiveObj(refObj, CanvasList[(int)Type.Interactive]));
    }

    IEnumerator GenerateInteractiveObj(GameObject refObj, GameObject canvas) 
    {
        yield return new WaitForSeconds(4f);
        GameObject obj = Instantiate(refObj, canvas.transform.GetChild(2).gameObject.transform);
        obj.transform.localPosition = spawnCoords[Type.Interactive];
        obj.GetComponent<InteractiveBuildings>().isTrigger = true;
        SetConfigToObj(ref obj, speeds[Type.Interactive], canvas, Type.Interactive);
    }
}
