using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSpeed : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] private AudioSource audio;
    private Image image;
    private LevelSpeed levelSpeed;

    private void Start()
    {
        levelSpeed = FindObjectOfType<LevelSpeed>();
        image = GetComponent<Image>();

        levelSpeed.OnSpeedChanged += CheckSpeed;
    }

    public void NextSpeed()
    {
        levelSpeed.NextSpeed();
        audio.Play();
    }

    private void CheckSpeed()
    {
        SetIcon(levelSpeed.currentState); 
    }

    private void SetIcon(int currentState)
    {
        image.sprite = sprites[currentState];
    }
}
