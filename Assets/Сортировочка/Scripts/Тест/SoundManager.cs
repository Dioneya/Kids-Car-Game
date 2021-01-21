using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum DetailSoundType { Metall, Glass, Police, Sportcar ,None };

    public static List<AudioClip> DetailSoundsPickup = new List<AudioClip>();
    public static List<AudioClip> DetailSoundsPut = new List<AudioClip>();
    public static AudioClip DetailError;
    [SerializeField] private AudioClip garageMusic;
    [SerializeField] private AudioClip engineStart;
    [SerializeField] private AudioClip startRide;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private List<AudioClip> _DetailSoundsPickup = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _DetailSoundsPut = new List<AudioClip>();
    [SerializeField] private AudioClip _DetailError;

    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource interactiveMusic;
    void Start() 
    {
        InitializeDetailSounds();
        bgMusic.clip = garageMusic;
    }

    private void InitializeDetailSounds() 
    {
        DetailSoundsPickup = new List<AudioClip>(_DetailSoundsPickup);
        _DetailSoundsPickup.Clear();

        DetailSoundsPut = new List<AudioClip>(_DetailSoundsPut);
        _DetailSoundsPut.Clear();

        DetailError = _DetailError;
        _DetailError = null;
    }

    public AudioSource GetLvlAudioSource() 
    {
       return bgMusic;
    }
    public AudioSource GetInteractiveAudioSource() 
    {
        return interactiveMusic;
    }
    public IEnumerator AudioDecay()
    {
        for (float i = 1; i > 0; i -= 0.05f)
        {
            bgMusic.volume = i;
            yield return new WaitForSeconds(0.3f);
        }    
    }
    public IEnumerator StartGameplayMusic() 
    {
        bgMusic.clip = engineStart;
        bgMusic.volume = 1;
        bgMusic.Play();
        yield return new WaitForSeconds(2f);
        TurnRideMusic();
    }

    public void TurnRideMusic() 
    {
        bgMusic.loop = true;
        bgMusic.clip = startRide;
        bgMusic.Play();
    }


    public void TurnVictoryMusic()
    {
        bgMusic.loop = false;
        bgMusic.clip = victoryMusic;
        bgMusic.Play();
    }

    public void TrunInteractiveMusic(AudioClip audio)
    {
        bgMusic.Pause();
        interactiveMusic.clip = audio;
        interactiveMusic.Play();
    }
    public void TurnOffInteractMusic() 
    {
        interactiveMusic.Stop();
        bgMusic.Play();
    }
}
