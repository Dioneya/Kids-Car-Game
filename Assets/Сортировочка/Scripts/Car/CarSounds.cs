using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    [SerializeField] List<AudioClip> engineSound;
    [SerializeField] AudioClip stopSound;
    bool disableNextStopSound;

    LevelSpeed levelSpeed;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        levelSpeed = FindObjectOfType<LevelSpeed>();
        levelSpeed.OnSpeedChanged += SwitchAudio;
        disableNextStopSound = false;
        levelSpeed.OnCarStop += StopCar;
    }

    private void SwitchAudio()
    {
        source.clip = engineSound[levelSpeed.currentState];
        source.loop = true;
        source.volume = 0.75f;
        source.Play();
    }

    private void StopCar()
    {
        if (disableNextStopSound)
        {
            disableNextStopSound = false;
            return;
        }
        source.volume = 1f;
        source.clip = stopSound;
        source.loop = false;
        source.Play();
    }

    public void Mute()
    {
        source.mute = true;
    }

    public void Unmute()
    {
        source.mute = false;
    }

}
