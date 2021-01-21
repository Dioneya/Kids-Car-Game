using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviableCheck : MonoBehaviour
{
    public bool isAviable = false;
    [SerializeField] private Sprite aviableCar;
    [SerializeField] private Sprite lockedCar;
    [SerializeField] private Sprite aviablePlatform;
    [SerializeField] private Sprite lockedPlatform;
    private GameObject lockObj;
    private SpriteRenderer carRender;
    private SpriteRenderer platformRender;
    private void Awake() 
    {
        GameObject car = transform.GetChild(0).gameObject;
        carRender = car.GetComponent<SpriteRenderer>();
        platformRender = car.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
        if(!isAviable)
        {
            lockObj = transform.GetChild(1).gameObject;
            lockObj.SetActive(true);
            carRender.sprite = lockedCar;
            platformRender.sprite = lockedPlatform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
