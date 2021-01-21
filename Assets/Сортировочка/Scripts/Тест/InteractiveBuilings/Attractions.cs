using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractions : MonoBehaviour, IInteractiveBuilding
{
    private GameObject tourist;
    private GameObject rider;
    private GameObject car;
    private LevelManager Level;
    private Animator playerAnim;
    [SerializeField] private GameObject photo;
    MissionProgress mission;
    private SoundManager music = null;
    [SerializeField] private AudioClip musicBg;
    void Start() 
    {
        Level = LevelManager.GetLevelManager();
        mission = Level.gameObject.GetComponent<MissionProgress>();
        rider = Level.GetRider();
        car = Level.car;
        tourist = Level.character.gameObject;
        playerAnim = tourist.GetComponent<Animator>();
    }
    void IInteractiveBuilding.Action()
    {
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
        charact.GoTo(this.transform.position);

        yield return new WaitWhile(() => charact.isMoving);
        playerAnim.Play("Idle");
        ShowPhotoCloud();
    }

    void ShowPhotoCloud()
    {
        photo.SetActive(true);
    }
    public void CloudClicked()
    {
        StartCoroutine(TakePhoto());
        photo.SetActive(false);
        StartCoroutine(WaitForEndPhoto());
    }

    IEnumerator TakePhoto() 
    {
        tourist.GetComponent<Animator>().Play("Interactive");
        AudioSource source = tourist.GetComponent<AudioSource>();
        yield return new WaitForSeconds(2.01f);
        source.Play();
        yield return new WaitForSeconds(.25f);
        source.Play();
        yield return new WaitForSeconds(.25f);
        source.Play();
        yield return new WaitForSeconds(1.5f);
        source.Play(); 
        yield return new WaitForSeconds(.14f);
        source.Play();
        yield return new WaitForSeconds(.26f);
        source.Play();
        yield return new WaitForSeconds(.26f);
        source.Play();
    }

    
    IEnumerator WaitForEndPhoto() 
    {
        yield return new WaitForSeconds(6f);
        playerAnim.Play("Walk");
        tourist.GetComponent<Character>().GoTo(car.transform.position);
        yield return new WaitWhile(() => tourist.GetComponent<Character>().isMoving);
        tourist.SetActive(false);
        rider.SetActive(true);
        LevelManager.isMoved = true;

        music.TurnOffInteractMusic();
        Unlock();

        mission.TaskComplete();
    }

    void Unlock()
    {
        GameObject obj = Level.GetGameplayBtn();
        obj.SetActive(true);
    }
}
