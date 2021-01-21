using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbersSound : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] private AudioSource _timerAudio;
    [SerializeField] private AudioClip _catchingRobber;
    [SerializeField] private AudioClip _releaseTheRobber;
    [SerializeField] private AudioClip _findRobber;
    [SerializeField] private AudioClip _insideCar;

    public void PlayAudioCatching()
    {
        _audioSource.loop = true;
        _audioSource.clip = _catchingRobber;
        _audioSource.Play();
    }
    public void PlayAudioRobberLose()
    {
        _audioSource.loop = false;
        _audioSource.clip = _releaseTheRobber;
        _audioSource.Play();
    }
    public void PlayAudioFind()
    {
        _audioSource.loop = false;
        _audioSource.clip = _findRobber;
        _audioSource.Play();
    }
    public void InsideInteractive()
    {
        _audioSource.loop = false;
        _audioSource.clip = _insideCar;
        _audioSource.Play();
    }
    public void PlayTimerAudio(bool muteAudio)
    {
        if (!muteAudio)
        {
            _timerAudio.mute = false;
            _timerAudio.loop = true;
            _timerAudio.Play();
        }
        else
        {
            _timerAudio.loop = false;
            _timerAudio.mute = true;
            _timerAudio.Stop();
        }
    }
}
