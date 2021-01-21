using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightAction : MonoBehaviour, IInteractiveAction
{
    [SerializeField] private Image light;
    [SerializeField] private AudioClip audio;
    private AudioSource audioSource;
    private bool isDay = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void Action()
    {
        StartCoroutine(Act());
    }

    public void TurnOff()
    {
        light.enabled = false;
    }

    public void TurnOn()
    {
        light.enabled = true;
    }

    void Update() 
    {
        if (isDay != LevelManager.isDay) 
        {
            if (!LevelManager.isDay)
                TurnOn();
            else
                TurnOff();
            isDay = LevelManager.isDay;
        }
    }

    IEnumerator Act()
    {
        audioSource.clip = audio;
        for (int i = 0; i < 2; i++) 
        {
            audioSource.Play();
            TurnOn();
            yield return new WaitForSeconds(0.5f);
            TurnOff();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
