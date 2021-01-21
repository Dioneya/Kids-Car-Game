using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject vibro;
    [SerializeField] GameObject music;

    [SerializeField] private Sprite[] _musicSprite;
    [SerializeField] private Sprite[] _vibraSprite;
    private bool isOpened = false;
    private bool isMusicActive = true;
    private bool isVibroActive = true;

    AudioSource musicSource;

    private void Start()
    {
        musicSource = FindObjectOfType<Level>().GetComponent<AudioSource>();
    }

    public void IconClicked()
    {
        if (isOpened)
        {
            isOpened = false;
            menu.SetActive(false);
        }
        else
        {
            isOpened = true;
            menu.SetActive(true);
        }
    }

    public void MusicClicked()
    {
        if (isMusicActive)
        {
            isMusicActive = false;
            //AudioListener.pause = true;
            musicSource.mute = true;
            music.GetComponent<Image>().sprite = _musicSprite[1];
        }
        else
        {
            isMusicActive = true;
            musicSource.mute = false;
            //AudioListener.pause = false;
            music.GetComponent<Image>().sprite = _musicSprite[0];
        }
    }

    public void VibroClicked()
    {
        if (isVibroActive)
        {
            isVibroActive = false;
            Vibration.Block();
            vibro.GetComponent<Image>().sprite = _vibraSprite[1];
        }
        else
        {
            isVibroActive = true;
            Vibration.UnBlock();
            vibro.GetComponent<Image>().sprite = _vibraSprite[0];
        }
    }

    public void DifficultClicked(int type)
    {
        if (type < 0 || type > 2)
            throw new Exception();

        LevelSettings.Instance.difficultLevel = (LevelSettings.Difficult) type;
        FindObjectOfType<SceneController>().ReloadScene();
    }
}
