using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeDay : MonoBehaviour
{
    LevelMover level;
    Image image;
    [SerializeField] Sprite day;
    [SerializeField] Sprite night;
    private bool isReady = true;
    void Start()
    {
        level = FindObjectOfType<LevelMover>();
        image = GetComponent<Image>();
        TurnDay();
    }

    private void OnEnable() 
    {
        isReady = true;
        if(image != null)
            SpriteCheck();
    }
    public void OnClick()
    {
        /*if (level.isDay)
            TurnNight();
        else
            TurnDay();*/
        if (isReady) 
        {
            if (LevelManager.isDay)
                TurnNight();
            else
                TurnDay();
            StartCoroutine(Wait());
        }
        
    }

    private void SpriteCheck() 
    {
        image.sprite = LevelManager.isDay ? night : day;
    }

    private void TurnDay()
    {

        image.sprite = night;
        LevelManager.isDay = true;
    }

    private void TurnNight()
    {
       image.sprite = day;
       LevelManager.isDay = false;
    }

    IEnumerator Wait() 
    {
        isReady = false;
        yield return new WaitForSeconds(2f);
        isReady = true;
    }
}
