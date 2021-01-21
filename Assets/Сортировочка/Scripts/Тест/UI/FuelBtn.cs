using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBtn : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] float[] timeForReduce = new float[3] { 9f, 8f, 4f };
    [SerializeField] AZSBtn azs;
    public static int fuel = 8;
    public bool isFill = false;

    private Animator animator;
    [SerializeField] private List<Sprite> spriteFlash = new List<Sprite>();

    private Image image;

    void Awake() 
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
    void Start() 
    {
        animator.enabled = false;
        isFill = false;
        if (Settings.difficult == Settings.Difficult.Easy)
            fuel = 8;
        else if (Settings.difficult == Settings.Difficult.Medium)
            fuel = 6;
        else
            fuel = 3;
    }

    public void FuelBtnClick() 
    {
        if (fuel<8 && !isFill) 
        {
            isFill = true;
            azs.OnBtnClicked();
            StopAllCoroutines();
        }
    }

    public IEnumerator FuelReduction()
    {
        while (fuel > 0) 
        {
            yield return new WaitForSeconds(timeForReduce[LevelManager.lvlSpeed]);
            if (!LevelManager.isMoved)
                break;
            fuel--;
            if(fuel>1)
                image.sprite = sprites[fuel];
            
            else if(fuel == 1) // Включить анимацию мигания индикатора предпоследнего деления
            {
                animator.enabled = true;  
                animator.Play("Fuel1"); 
            }
                
            else if(fuel==0) // Включить анимацию мигания индикатора последнего деления
                animator.Play("Fuel");
            Debug.LogWarning(fuel);
        }
        azs.OnBtnClicked();
    }
    public void ChangeSprite() 
    {
        animator.enabled = false;  
        image.sprite = sprites[fuel];
    }
    void OnEnable() 
    {
        StartCoroutine(FuelReduction());
        image.sprite = sprites[fuel];
    }

}
