using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepBtn : MonoBehaviour
{
    [SerializeField] private LvlGenerator generator;
    [SerializeField] private GameObject gameplayBtn;
    private Button btn;
    void Awake() 
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnClicked);
    }

    public void OnBtnClicked() 
    {
        generator.AddInteractiveToQueue("Motel");
        LevelManager.isDay = false;
        gameplayBtn.SetActive(false);
    }
}
