﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class IceBar : MonoBehaviour
{
    public static UnityEvent addValue;
    public static UnityEvent removeValue;
    [SerializeField]
    Sprite[] sprites;
    Image image;
    public static int value = 0;
    private int tempValue = 0;
    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        if (addValue == null)
            addValue = new UnityEvent();
        if (removeValue == null)
            removeValue = new UnityEvent();
        value = 2;
        ChangeSprite();
        image.enabled = false;
        addValue.AddListener(AddValue);
        removeValue.AddListener(RemoveValue);
        StartCoroutine(WaiterForUnlock());
    }
    void OnDestroy()
    {
        addValue.RemoveAllListeners();
        removeValue.RemoveAllListeners();
    }
    
    IEnumerator WaiterForUnlock() 
    {
        yield return new WaitWhile(()=>!LevelManager.isMoved);
        image.enabled = true;
    }
    public void AddValue()
    {
        tempValue++;
        bool easyMode = tempValue == 1 && Settings.difficult == Settings.Difficult.Easy;
        bool normalMode = tempValue == 2 && Settings.difficult == Settings.Difficult.Medium;
        bool hardMode = tempValue == 3 && Settings.difficult == Settings.Difficult.Hard;

        if (easyMode || normalMode || hardMode)
        {
            value++;
            tempValue = 0;
        }

        ChangeSprite();
    }

    public void RemoveValue() 
    {
        value--;
        ChangeSprite();
    }
    void ChangeSprite()
    {
        image.sprite = sprites[value];
    }
}
