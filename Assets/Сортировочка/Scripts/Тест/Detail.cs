using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Detail : MonoBehaviour
{
    public string nameOfDetail;
    public Vector3 spawnScale;
    public Vector3 spawnPosition;
    public Vector3 spawnLocalPosition;

    public SoundManager.DetailSoundType SoundType;

    private AudioSource audioSource;
    bool isCorrectPut;
    void Awake() 
    {
        gameObject.transform.localScale = spawnScale;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayPickupSound() 
    {
        PlaySound(SoundManager.DetailSoundsPickup[(int)SoundType]);
    }

    public void PlayPutSound() 
    {
        PlaySound(SoundManager.DetailSoundsPut[(int)SoundType]);
        isCorrectPut = true;
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy() 
    {
        gameObject.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    public void PlayErrorSound()
    {
        if (!isCorrectPut) 
        {
            PlaySound(SoundManager.DetailError);
            //if(Settings.isVibrate) Handheld.Vibrate();
        }
            
    }

    private void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}

