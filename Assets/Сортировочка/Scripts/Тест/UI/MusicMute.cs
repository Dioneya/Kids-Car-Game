using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMute : MonoBehaviour
{

    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private AudioSource music;

    private Image image;
    void Awake() 
    {
        image = GetComponent<Image>();
    }
    void Start() 
    {
        ReplaceSpriteAndSourceMuteStatus();
    }
    public void ChangeMuteStatus()
    {
        Settings.isMuted = !Settings.isMuted;
        music.mute = Settings.isMuted;
        ReplaceSpriteAndSourceMuteStatus();
    }

    void ReplaceSpriteAndSourceMuteStatus()
    {
        music.mute = Settings.isMuted;
        image.sprite = Settings.isMuted ? soundOff : soundOn;
    }
}
