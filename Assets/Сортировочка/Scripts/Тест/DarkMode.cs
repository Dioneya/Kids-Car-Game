using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DarkMode : MonoBehaviour
{
    private Image image;
    private Image DarkImage;
    private bool isDay = true;
    [SerializeField] private bool isHasDarkMode = true;
    void Awake()
    {
        image = GetComponent<Image>();
        if(isHasDarkMode)
            DarkImage = gameObject.transform.parent.gameObject.GetComponent<Image>();
    }
    void Start() 
    {
        isDay = LevelManager.isDay;
        if (!isDay) 
        {
            var c = image.color;
            c.a = 0;
            image.color = c;
        }
        HideImage();
            
    }
    void FixedUpdate() 
    {
        if (isDay == LevelManager.isDay)
            return;
        else 
        {
            isDay = LevelManager.isDay;
            Debug.LogWarning("Переход день:" + isDay);
            ChangeMode();
        }  
    }
    
    public void ChangeMode() 
    {
        if (isDay)
        {
            StartCoroutine(ChangeMode(0, 1, 0.02f));
        }
        else 
        {
            StartCoroutine(ChangeMode(1, 0, -0.02f));
        }
    }

    IEnumerator ChangeMode(float start, float end, float step)
    {
        if(isHasDarkMode)
            DarkImage.enabled = true;
        image.enabled = true;
        for (float i = start+step; i != end; i += step)
        {
            yield return new WaitForSeconds(0.02f);
            var c = image.color;
            c.a = i;
            image.color = c;
            if (image.color.a >= 1 || image.color.a <= 0)
                break;
        }
        HideImage();
    }

    private void HideImage() 
    {
        if (isDay) 
        {
            if (isHasDarkMode)
                DarkImage.enabled = false;
        }
        else
            image.enabled = false;
    }
}
