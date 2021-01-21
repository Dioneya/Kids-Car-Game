using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSettings : MonoBehaviour
{
    public Vector2 smallSize = Vector2.one;
    public bool takeAllPlace = false;
    public SoundType soundType;
    public string audioPath;
    public string destinationObjectTag;
    public float destionationObjectRangeInstall;
    public bool isColorMatching;
    public GameObject targetObject;
    private List<AudioClip> sounds;
    private AudioClip errorSound;
    //private AudioSource source;
    private void Start()
    {

        sounds = new List<AudioClip>(Resources.LoadAll<AudioClip>(audioPath + "/" + soundType.ToString()));
        errorSound = Resources.Load<AudioClip>(audioPath + "/Error/Error");
    }

    public AudioClip PlayPickUp()
    {
        if (sounds.Count == 0)
            return null;
        return sounds[0];
    }

    public AudioClip PlayPut()
    {
        if (sounds.Count <= 1)
            return null;
        return sounds[1];
    }

    public AudioClip PlayError()
    {
        return errorSound;
    }

    public enum SoundType { Metall,Glass,Police, None }
}
