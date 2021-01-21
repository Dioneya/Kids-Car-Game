using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBtn : MonoBehaviour
{
    [SerializeField] private Sprite[] SpeedsSprite;
    [SerializeField] private CarV2 car;

    public bool isWork = true;

    private Image image;
    private AudioSource audio;
    
    void Awake()
    {
        image = GetComponent<Image>();
        audio = GetComponent<AudioSource>();
    }
    void Start() 
    {
        isWork = true;
    }
    public void ChangeSpeedStatus()
    {
        if (!isWork)
            return;
        LevelManager.lvlSpeed++;
        if (LevelManager.lvlSpeed > 2)
        {
            LevelManager.lvlSpeed = 0;
            StartCoroutine(car.MoveBack());
        }
        else 
        {
            StartCoroutine(car.MoveForward());
        }
            
        if (Settings.isVibrate) Vibration.Vibrate(100);
        image.sprite = SpeedsSprite[LevelManager.lvlSpeed];
        audio.Play();
        car.ChangeMotorSound(LevelManager.lvlSpeed);
    }
    
}
