using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinkerAction : MonoBehaviour, IInteractiveAction
{
    public event System.Action OnWinkerOff;

    [SerializeField] Sprite red;
    [SerializeField] Sprite blue;
    [SerializeField] HardLight2D _hardLight2DBlue;
    [SerializeField] HardLight2D _hardLight2DRed;
    [SerializeField] SpriteRenderer redLight;
    [SerializeField] SpriteRenderer blueLight;
    [SerializeField] AudioClip audio;
    [SerializeField] AudioClip audioSiren;

    private Sprite standart;
    private SpriteRenderer renderer;
    private AudioSource audioSource;
    private bool isWorking = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        renderer = GetComponent<SpriteRenderer>();
        standart = renderer.sprite;
    }

    public void Action()
    {
        StartCoroutine(Act());
    }

    public void TurnOff()
    {
        audioSource.loop = false;
        isWorking = false;
        _hardLight2DRed.Intensity = 0;
        _hardLight2DBlue.Intensity = 0f;
        OnWinkerOff?.Invoke();
    }

    public void TurnOn()
    {
        if (audioSource.isPlaying)
            return;
        audioSource.clip = audioSiren;
        audioSource.loop = true;
        audioSource.Play();
        isWorking = true;
        StartCoroutine(Working());
    }

    public void SoundOff()
    {
        audioSource.Stop();
    }

    

    IEnumerator Working()
    {
        while (isWorking)
        {
            renderer.sprite = red;
            redLight.enabled = true;
            _hardLight2DBlue.Intensity = 0;
            _hardLight2DRed.Intensity = 0.7f;
            blueLight.enabled = false;
            yield return new WaitForSeconds(0.5f);
            renderer.sprite = blue;
            blueLight.enabled = true;
            _hardLight2DRed.Intensity = 0;
            _hardLight2DBlue.Intensity = 0.7f;
            redLight.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }

        _hardLight2DRed.Intensity = 0;
        _hardLight2DBlue.Intensity = 0f;
        audioSource.Stop();
        redLight.enabled = false;
        blueLight.enabled = false;
        renderer.sprite = standart;
    }

    IEnumerator Act()
    {
        audioSource.clip = audio;
        audioSource.Play();
        renderer.sprite = red;
        redLight.enabled = true;
        _hardLight2DRed.Intensity = 0.7f;
        yield return new WaitForSeconds(0.5f);
        blueLight.enabled = true;
        _hardLight2DRed.Intensity = 0;
        _hardLight2DBlue.Intensity = 0.7f;
        redLight.enabled = false;
        renderer.sprite = blue;
        yield return new WaitForSeconds(0.5f);
        blueLight.enabled = false;
        renderer.sprite = standart;
        _hardLight2DBlue.Intensity = 0;
    }
}
