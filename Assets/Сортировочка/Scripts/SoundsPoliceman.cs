using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPoliceman : MonoBehaviour
{

    [SerializeField] AudioClip eatSound;
    [SerializeField] private AudioClip _soundWhistle;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayEatSound()
    {
        source.clip = eatSound;
        source.Play();
    }
    public void PlayWhistle()
    {
        source.clip = _soundWhistle;
        source.Play();
    }
}
