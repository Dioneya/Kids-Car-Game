﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabricaBtn : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private LvlGenerator generator;
    [SerializeField] private GameObject gameplayBtn;
    [SerializeField] private string nameOfAttract;
    [SerializeField] private bool isMoveToCenter = false;
    bool isAcitve = true;
    [SerializeField] private int value;
    private Button btn;
    public bool isFeedKids = false;

    private string[] text = new string[] { 
                                            "Parc", "Swing", "Lake" 
                                         }; 
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnClicked);
    }
    void OnEnable() 
    {
        GetComponent<Image>().sprite = IceBar.value == value ? sprites[0] : sprites[1];
        isAcitve = IceBar.value != value;
    }
    public void OnBtnClicked()
    {
        if (!isAcitve)
            return;
        if (isFeedKids)
            generator.AddInteractiveToQueue(text[Random.Range(0,text.Length)]);
        else 
            generator.AddInteractiveToQueue(nameOfAttract);
        gameplayBtn.SetActive(false);
        if (isMoveToCenter)
            LevelManager.GetLevelManager().car.GetComponent<CarV2>().MoveToCenter();
    }
}
