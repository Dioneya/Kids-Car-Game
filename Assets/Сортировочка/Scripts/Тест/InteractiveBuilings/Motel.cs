using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Motel : MonoBehaviour, IInteractiveBuilding
{
    private GameObject tourist;
    private GameObject rider;
    private GameObject car;
    private LevelManager Level;

    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject Zzz;
    [SerializeField] private Sprite[] windows;
    [SerializeField] private Vector3[] zzzCoord;
    [SerializeField] private AudioClip musicBg;
    Vector3 posToGo;
    private SoundManager music;

    void IInteractiveBuilding.Action()
    {
        Level = LevelManager.GetLevelManager();
        rider = Level.GetRider();
        car = Level.car;
        tourist = Level.character.gameObject;
        Vector3 newPos = new Vector3(car.transform.position.x, tourist.transform.position.y, tourist.transform.position.z);
        tourist.transform.position = newPos;
        tourist.SetActive(true);
        rider.SetActive(false);
        music = Level.GetSoundManager();
        music.TrunInteractiveMusic(musicBg);

        StartCoroutine(WaitForCharStop(tourist.GetComponent<Character>()));
    }

    IEnumerator WaitForCharStop(Character charact)
    {
        posToGo = transform.position;
        posToGo.x -= 1f;
        charact.GoTo(posToGo);

        yield return new WaitWhile(() => charact.isMoving);
        tourist.GetComponent<Animator>().Play("Idle");
        ShowBedCloud();
    }

    private void ChangeWindowLight() 
    {
        System.Random rnd = new System.Random();
        int ind = rnd.Next(windows.Length);
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = windows[ind];
        Zzz.transform.localPosition = zzzCoord[ind];

    }

    void ShowBedCloud() 
    {
        bed.SetActive(true);
    }

    public void CloudClicked() 
    {
        tourist.SetActive(false);
        StartCoroutine(MoneyTransform());
        bed.SetActive(false);
    }

    IEnumerator MoneyTransform() 
    {
        money.SetActive(true);
        Vector3 currentStart = money.transform.localPosition;
        Vector3 endPos = currentStart;
        endPos.y = 102f;

        for (float i = 0; i <= 1f; i += 0.02f) 
        {
            yield return new WaitForSeconds(0.02f);
            money.transform.localPosition = Vector3.Lerp(currentStart, endPos, i);
        }

        money.SetActive(false);
        StartCoroutine(WaitForSleep());
    }

    IEnumerator WaitForSleep() 
    {
        yield return new WaitForSeconds(2f);
        GetComponent<AudioSource>().Play();
        ChangeWindowLight();
        yield return new WaitForSeconds(1f);
        Zzz.SetActive(true);
        yield return new WaitForSeconds(7f);
        Zzz.SetActive(false);
        LevelManager.isDay = true;
        yield return new WaitForSeconds(2f);
        music.GetInteractiveAudioSource().Stop();
        tourist.SetActive(true);
        tourist.GetComponent<Character>().GoTo(car.transform.position);
        yield return new WaitWhile(() => tourist.GetComponent<Character>().isMoving);
        tourist.SetActive(false);
        rider.SetActive(true);

        music.GetComponent<SoundManager>().TurnRideMusic();
        LevelManager.isMoved = true;
        Unlock();
    }

    void Unlock() 
    {
        GameObject obj = Level.GetGameplayBtn();
        obj.SetActive(true);
    }
}
