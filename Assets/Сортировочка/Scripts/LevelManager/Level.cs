using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    GameObject garageBack;
    [SerializeField]
    private LevelGenerator generator;
    LevelSpeed levelSpeed;
    UI ui;
    Car car;
    public AudioSource audio;
    public CharacterMovement character;
    bool isCharacterSeated = false;

    [SerializeField] AudioClip driveMusic;
    [SerializeField] AudioClip endineStartMusic;
    [SerializeField] AudioClip victoryMusic;

    private void Start()
    {
       // character = GameObject.FindGameObjectWithTag("Character");
        character.OnTargetReached += CharacterSit;
        audio = GetComponent<AudioSource>();
        garageBack = GameObject.FindGameObjectWithTag("GarageBack");
        car = FindObjectOfType<Car>();
        ui = FindObjectOfType<UI>();
        levelSpeed = GetComponent<LevelSpeed>();
       // generator = GetComponent<LevelGenerator>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
    }
#endif

    private void CharacterSit()
    {
        isCharacterSeated = true;
    }

    public void StartLevel()
    {
        garageBack = GameObject.FindGameObjectWithTag("GarageBack");
        StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        //car.EnableMoving();
        //yield return new WaitForSeconds(3f);
        //ui.DarkScreen();
        generator.enabled = true;
        yield return new WaitForSeconds(2f);
        //ui.RemoveDark();
        //car.DisableMoving();
        car.DisableCarBorders();


        var startScale = car.transform.localScale;
        var startPos = car.transform.position;
        var endScale = new Vector3(0.7f, 0.7f, 0);
        var endPos = new Vector3(-3.5f, -3.4f, 0);

        var gStartScale = garageBack.transform.localScale;
        var gStartPos = garageBack.transform.position;
        var gEndScale = new Vector3(1.25f, 1.25f, 0);
        var gEndPos = new Vector3(-13f, -0.5f, 1);


        car.EnableWheels();

        for (float i = 0; i < 1.1f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            car.transform.localScale = Vector3.Lerp(startScale,endScale,i);
            car.transform.position = Vector3.Lerp(startPos,endPos,i);

            garageBack.transform.localScale = Vector3.Lerp(gStartScale, gEndScale, i);
            garageBack.transform.position = Vector3.Lerp(gStartPos, gEndPos, i);
        }
        //garageBack.SetActive(false);

        StartCoroutine(AudioDecay());

        startPos = car.transform.position;
        endPos = new Vector3(-3.5f, car.transform.position.y, 0);

        character.GetComponent<CharacterMovement>().enabled = true;
        character.GetComponent<CharacterMovement>().GoTo(car.transform.position);

        for (float i = 0; i < 1.1f; i += 0.01f)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Going char");
            if (isCharacterSeated)
                break;
           // car.transform.position = Vector3.Lerp(startPos, endPos, i);
            
        }

        character.gameObject.SetActive(false);
        car.DriverSit();
        levelSpeed.SetSpeedLevel(0,true);
        car.EnableStars();
        ui.TurnOnGameplayUI(0);
        try
        {
            FindObjectOfType<DreamSpawner>().UnBlock(false);
        }
        catch
        {
            
        }
    }

    IEnumerator AudioDecay()
    {
        for(float i = 1;i>0;i-= 0.05f)
        {
            audio.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
        audio.clip = endineStartMusic;
        audio.volume = 1;
        audio.Play();
        yield return new WaitForSeconds(2f);
        audio.clip = driveMusic;
        audio.Play();
    }

    public void TurnVictoryMusic()
    {
        audio.loop = false;
        audio.clip = victoryMusic;
        audio.Play();
    }

    public void TurnDriveMusic()
    {
        audio.loop = true;
        audio.clip = driveMusic;
        audio.Play();
    }
}
