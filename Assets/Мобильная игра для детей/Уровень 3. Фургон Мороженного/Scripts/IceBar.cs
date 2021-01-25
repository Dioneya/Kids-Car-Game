using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class IceBar : MonoBehaviour
{
    public static UnityEvent addValue;

    [SerializeField]
    Sprite[] sprites;
    Image image;
    public static int value = 0;
    
    void Awake() 
    {
        image = GetComponent<Image>();
    }

    void Start() 
    {
        if (addValue == null)
            addValue = new UnityEvent();
        value = 2;
        ChangeSprite();
        addValue.AddListener(AddValue);

    }
    void OnDestroy() 
    {
        addValue.RemoveAllListeners();
    }

    public void AddValue() 
    {
        value++;
        ChangeSprite();
    }

    void ChangeSprite() 
    {
        image.sprite = sprites[value];
    }
}
