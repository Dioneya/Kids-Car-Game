using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionProgress : MonoBehaviour
{
    [SerializeField] private float hardNumOfInteractive = 9.0f;
    [SerializeField] private float mediumNumOfInteractive = 6.0f;
    [SerializeField] private float easyNumOfInteractive = 3.0f;
    [SerializeField] private static BaloonGenerator baloonGenerator;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Slider progressBar;
    private AudioSource audioSource;
    public float targetProgress;
    private float fillSpeed = .5f;
    private bool startFill = false;
    private bool startReset = false;
    void Awake() 
    {
        audioSource = progressBar.gameObject.GetComponent<AudioSource>();
    }
    void Start() 
    {

        if (Settings.difficult == Settings.Difficult.Easy)
            progressBar.maxValue = 3.0f;
        else if (Settings.difficult == Settings.Difficult.Medium)
            progressBar.maxValue = 6.0f;
        else if (Settings.difficult == Settings.Difficult.Hard)
            progressBar.maxValue = 9.0f;

        baloonGenerator = FindObjectOfType<BaloonGenerator>();
        baloonGenerator.enabled = false;
        
        //progressBar.maxValue = maxProgress;
    }

    void Update()
    {
        if (progressBar.value < targetProgress && startFill)
        {
            progressBar.value += fillSpeed * Time.deltaTime;
            if (progressBar.value >= targetProgress)
                startFill = false;
        }

        if (progressBar.value > targetProgress && startReset) 
        {
            progressBar.value -= 1.5f * Time.deltaTime;
            if (progressBar.value <= targetProgress)
                startReset = false;
        } 

    }

    public void TaskComplete() 
    {
        targetProgress = targetProgress+1.0f;
        startFill = true;
        if (Settings.isVibrate) Vibration.Vibrate(200);
        audioSource.Play();
        if (targetProgress >= progressBar.maxValue)
        {
            LevelManager.isMoved = false;
            Victory();
        }
        
    }
    public void Reset() 
    {
        targetProgress = 0.0f;
        startReset = true;
    }
    private void Victory() 
    {
        StartCoroutine(GetComponent<LevelManager>().RestartLvl());
        soundManager.TurnVictoryMusic();
        baloonGenerator.enabled = true;
        FuelBtn.fuel = 8;
    }


}
