using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static bool isStart = false;
    public static int lvlSpeed = 0;
    public static bool isMoved = false;
    public static bool isInteract = false;
    public static bool isDay = true;

    static LevelManager levelManager;

    [SerializeField] public Character character;
    public GameObject car;
    private MissionProgress missionProgress;
    [SerializeField] private GameObject rider;
    [SerializeField] private GameObject gameplayButtons;
    [SerializeField] private GameObject GarageBG;
    [SerializeField] private GameObject pistolet;
    [SerializeField] private GameObject shlang;
    [SerializeField] private Slider progressBar;
    [SerializeField] private List<GameObject> InteractiveBtns = new List<GameObject>(3);
    [SerializeField] private List<GameObject> listOfTransofrmedObj = new List<GameObject>(); //Порядок элементов такой же как и в словарях (см.комментарии к их значениям)
    /*Dictionary<int, Vector3> endScales = new Dictionary<int, Vector3>() 
    {
        {0, new Vector3(.7f, .7f, 1) }, //Garage
        {1, new Vector3(0.7f, 0.7f, 1)}, //Car
        {2, new Vector3(0.7f, 0.7f, 1)}, //BackBuildings
        {3, new Vector3(0.7f, 0.7f, 1)}, //Buildings
        {4, new Vector3(0.7f, 0.7f, 1)}, //BackCity
        {5, new Vector3(0.7f, 0.7f, 1)}, //Clouds
        {6, new Vector3(0.7f, 0.7f, 1)}, //Road
    };
*/
    [SerializeField] private SoundManager soundManager;
    void Awake() 
    {
        missionProgress = GetComponent<MissionProgress>();
    }
    void Start() 
    {
        isStart = false;
        isDay = true;
        isMoved = false;
        lvlSpeed = 0;
        progressBar.gameObject.SetActive(false);
        gameplayButtons.SetActive(false);
        levelManager = this;
        StartCoroutine(WaitForStart());
    }

    #region Гэттеры
    public GameObject GetGameplayBtn() 
    {
        return gameplayButtons;
    }
    public GameObject GetPistolObj() 
    {
        return pistolet;
    }
    public static LevelManager GetLevelManager()
    {
        return levelManager;
    }
    public GameObject GetRider() 
    {
        return rider;
    }
    public GameObject GetShlangObj() 
    {
        return shlang;
    }
    public SoundManager GetSoundManager() 
    {
        return soundManager;
    }
    #endregion

    #region Выезд из гаража + начало заезда
    public void StartLevel()
    {
        StartCoroutine(Starting());
    }

    IEnumerator WaitForStart() 
    {
        yield return new WaitWhile(() => !isStart);
        StartLevel();
    }

    
    IEnumerator Starting()
    {
        //generator.enabled = true;
        yield return new WaitForSeconds(2f);
        GarageBG.SetActive(false);
        StartCoroutine(soundManager.AudioDecay());
        for (int i = 0; i < listOfTransofrmedObj.Count; i++) 
        {
            GameObject obj = listOfTransofrmedObj[i];
            if (obj != null) 
            {
                if(i==0)
                {
                    StartCoroutine(SmoothScale(obj, obj.transform.localScale, new Vector3(.6f, .6f, 1)));
                    StartCoroutine(SmoothPosition(obj, obj.transform.position, new Vector3(0f, -0.3f, 1)));
                }
                else
                {
                    StartCoroutine(SmoothScale(obj, obj.transform.localScale, new Vector3(.7f, .7f, 1)));
                    StartCoroutine(SmoothPosition(obj, obj.transform.position, new Vector3(0f, 0.3f, 1)));
                }
                    
            }
        }
        yield return new WaitForSeconds(2f);

        CharacterGoTo(car.transform.position);

        yield return new WaitWhile(() => character.isMoving);
        StartCoroutine(soundManager.StartGameplayMusic());
        rider.SetActive(true);
        character.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        isMoved = true;
        gameplayButtons.SetActive(true);
        progressBar.gameObject.SetActive(true);
    }
    private void CharacterGoTo(Vector3 pos) 
    {
        character.isMoving = true;
        character.GoTo(pos);
    }
    #endregion

    #region Плавное перемещение и зум
    IEnumerator SmoothScale(GameObject obj, Vector3 startScale, Vector3 endScale) 
    {
        for (float i = 0; i < 1.1f; i+=0.01f) 
        {
            yield return new WaitForSeconds(0.01f);
            obj.transform.localScale = Vector3.Lerp(startScale, endScale, i);
        }
    }
    IEnumerator SmoothPosition(GameObject obj, Vector3 startPos, Vector3 endPos)
    {
        for (float i = 0; i < 1.1f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            obj.transform.position = Vector3.Lerp(startPos, endPos, i);
        }
    }
    #endregion

    public IEnumerator RestartLvl() 
    {
        gameplayButtons.SetActive(false);
        yield return new WaitForSeconds(20f);
        gameplayButtons.SetActive(true);
        soundManager.TurnRideMusic();
        missionProgress.Reset();
        progressBar.GetComponent<AudioSource>().Play();
        isMoved = true;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
            isDay = !isDay;
    }
#endif

}




