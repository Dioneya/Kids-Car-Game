using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AZSBtn : MonoBehaviour
{
    [SerializeField] private LvlGenerator generator;
    [SerializeField] private GameObject gameplayBtn;
    private Image image;
    private Button btn;
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnClicked);
        image = GetComponent<Image>();
    }
    /*void OnEnable() 
    {
        if (FuelBtn.fuel == 10) 
        {
            var c = image.color;
            c.a = .5f;
            image.color = c;
            btn.enabled = false;

            StartCoroutine(FuelActive());
        }

    }*/

    void HideButtons(bool isHide)
    {
        for (int i = 0; i < gameplayBtn.transform.childCount; i++) 
        {
            GameObject btn = gameplayBtn.transform.GetChild(i).gameObject;
            if (btn.name != "Fuel") 
            {
                btn.SetActive(!isHide);
            }
        }
    }
    /*IEnumerator FuelActive() 
    {
        yield return new WaitWhile(()=>FuelBtn.fuel==10);
        btn.enabled = true;
        var c = image.color;
        c.a = 1f;
        image.color = c;
    }*/

    public void OnBtnClicked()
    {
        LevelManager.GetLevelManager().car.GetComponent<CarV2>().MoveToCenter();
        generator.AddInteractiveToQueue("AZS");
        HideButtons(true);
    }
}
