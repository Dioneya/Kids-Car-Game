using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAS : MonoBehaviour, IInteractiveBuilding
{
    private GameObject tourist, rider, car, pistol, shlang;
    private LevelManager Level;
    private Animator playerAnim;
    [SerializeField] private Image imageDark, imageLight;
    [SerializeField] private FuelBtn fuelBtn;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject forCharacter;
    [SerializeField] private Sprite[] spritesWithoutPistol;
    [SerializeField] private GameObject pistolMaker;
    Sprite withPistolLight, withPistolDark;
    private SoundManager music;
    [SerializeField] private AudioClip musicBg;
    private InteractiveBuildings interactive;

    void Awake() 
    {
        interactive = GetComponent<InteractiveBuildings>();
    }
    void Start()
    {
        withPistolLight = imageLight.sprite;
        withPistolDark = imageDark.sprite;

        if (interactive.isTrigger) fuelBtn = GameObject.Find("Fuel").GetComponent<FuelBtn>();
        Initialize();
    }

    private void Initialize() 
    {
        Level = LevelManager.GetLevelManager();
        rider = Level.GetRider();
        car = Level.car;
        pistol = Level.GetPistolObj();
        shlang = Level.GetShlangObj();
        tourist = Level.character.gameObject;
        playerAnim = tourist.GetComponent<Animator>();
    }

    void IInteractiveBuilding.Action()
    {
        Vector3 newPos = new Vector3(car.transform.position.x, tourist.transform.position.y, tourist.transform.position.z);
        tourist.transform.position = newPos;
        tourist.SetActive(true);
        rider.SetActive(false);
        RemoveMusic();
        
        StartCoroutine(WaitForCharStop(tourist.GetComponent<Character>()));
    }
    IEnumerator WaitForCharStop(Character charact)
    {
        charact.GoTo(forCharacter.transform.position);
        yield return new WaitWhile(() => charact.isMoving);
        StartWaitingForAction();
    }
    private void StartWaitingForAction()
    {
        playerAnim.Play("Idle");
        ShowPistol();
    }

    IEnumerator WaitForStartRefill()
    {
        yield return new WaitWhile(()=>!pistol.GetComponent<Pistol>().isWork);
        pistol.SetActive(false);
        ChangeAZSSprite(true);
        shlang.SetActive(true);
        Pistol pistolComp = pistol.GetComponent<Pistol>();
        pistol.transform.localPosition = pistolComp.spawnLocalPosition;
        pistolComp.isWork = false;
        StartCoroutine(MoneyTransform());
        StartCoroutine(FillTheTank());
    }

    IEnumerator FillTheTank()
    {
        while (FuelBtn.fuel < 8) 
        {
            yield return new WaitForSeconds(2f);
            FuelBtn.fuel++;
            fuelBtn.ChangeSprite();
        }
        StartCoroutine(EndInteractive());
    }

    IEnumerator EndInteractive()
    {
        ChangeAZSSprite(false);
        shlang.SetActive(false);

        playerAnim.Play("Walk");
        tourist.GetComponent<Character>().GoTo(car.transform.position);

        yield return new WaitWhile(() => tourist.GetComponent<Character>().isMoving);

        tourist.SetActive(false);
        rider.SetActive(true);

        music.TurnOffInteractMusic();
        LevelManager.isMoved = true;

        car.GetComponent<CarV2>().MoveToPrevPos();

        
        Unlock();
    }

    void ChangeAZSSprite(bool isWork) 
    {
        if (isWork)
        {
            imageDark.sprite = spritesWithoutPistol[0];
            imageLight.sprite = spritesWithoutPistol[1];
        }
        else 
        {
            imageDark.sprite = withPistolDark;
            imageLight.sprite = withPistolLight;
        }
    }

    IEnumerator MoneyTransform()
    {
        money.SetActive(true);
        Vector3 currentStart = money.transform.localPosition;
        Vector3 endPos = currentStart;
        endPos.y = 150f;

        for (float i = 0; i <= 1f; i += 0.02f)
        {
            yield return new WaitForSeconds(0.02f);
            money.transform.localPosition = Vector3.Lerp(currentStart, endPos, i);
        }

        money.SetActive(false);
    }

    private void RemoveMusic()
    {
        music = Level.GetSoundManager();
        music.TrunInteractiveMusic(musicBg);
    }

    void ShowPistol()
    {
        pistol.SetActive(true);
        pistol.transform.position = pistolMaker.transform.position;
        pistol.GetComponent<Pistol>().SetLocalPos();
        StartCoroutine(WaitForStartRefill());
    }

    void Unlock()
    {
        GameObject obj = Level.GetGameplayBtn();
        fuelBtn.gameObject.SetActive(false);
        for (int i = 0; i < obj.transform.childCount; i++) 
        {
            obj.transform.GetChild(i).gameObject.SetActive(true);
        }
        fuelBtn.isFill = false;
    }

    
}
