using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CarouselView : MonoBehaviour
{
    [SerializeField] private Button NextBtn, PrevBtn;
    [SerializeField] private PlayBtn playBtn; 
    private bool isActive = true;
    public UnityEvent OnSwitchNext, OnSwitchPerv, OnItemClicked;
    [SerializeField]private int currentLvl = 0;
    private CarouselItem[] items;

    void Start()
    {
        items = gameObject.transform.GetComponentsInChildren<CarouselItem>();
        NextBtn.onClick.AddListener(SwitchNextItem);
        PrevBtn.onClick.AddListener(SwitchPrevItem);
        Initialize();
    }
    private void Initialize() // Инициализация всех айтемов для корусели и добавление их в ивент
    {
        for (int i = 0; i < items.Length; i++) 
        {
            if(i==0)
                items[i].SetRelatedNodes(items[items.Length-1], items[i+1],i);
            else if(i==items.Length-1)
                items[i].SetRelatedNodes(items[i - 1], items[0],i);
            else 
                items[i].SetRelatedNodes(items[i-1], items[i+1],i);

            OnSwitchNext.AddListener(items[i].SwitchNext);
            OnSwitchPerv.AddListener(items[i].SwitchPrev);

            items[currentLvl].AddGlow();
        }
    }
    IEnumerator WaitTiming()
    {
        isActive = false;
        yield return new WaitForSeconds(.3f);
        isActive = true;
    }
    public void SwitchNextItem() 
    {
        if(isActive)
        {
            OnSwitchNext.Invoke();
            StartCoroutine(WaitTiming());
            items[currentLvl].RemoveGlow();
            currentLvl = currentLvl == items.Length-1 ? 0 : currentLvl+1;
            items[currentLvl].AddGlow();
            playBtn.CheckAviable(items[currentLvl]
                                    .GetComponent<AviableCheck>()
                                    .isAviable, currentLvl);
        }
    }

    public void SwitchPrevItem() 
    {
        if(isActive)
        {
            OnSwitchPerv.Invoke();
            StartCoroutine(WaitTiming());
            items[currentLvl].RemoveGlow();
            currentLvl = currentLvl == 0 ? items.Length-1 : currentLvl-1;
            items[currentLvl].AddGlow();
            playBtn.CheckAviable(items[currentLvl]
                                    .GetComponent<AviableCheck>()
                                    .isAviable, currentLvl);
        }
    }

    public void SwitchToClickedItem(int id) 
    {

        int left = FindLeftPath(id), right = FindRightPath(id);
        
        if (right >= left)
            StartMove(left, false);
        else
            StartMove(Math.Abs(right), true);
        Debug.Log(left+" "+right);
        items[currentLvl].RemoveGlow();
        currentLvl = id;
        items[currentLvl].AddGlow();
    }

    private int FindRightPath(int id) 
    {
        int cnt = 0;
        for (int i = currentLvl; i < items.Length; i++,cnt++) 
        {
            if (i == id)
                return cnt;
        }
        for (int i = 0; i < items.Length; i++, cnt++) 
        {
            if (i == id)
                return cnt;
        }
        return id;
    }

    private int FindLeftPath(int id)
    {
        int cnt = 0;
        for (int i = currentLvl; i > -1; i--, cnt++)
        {
            if (i == id)
                return cnt;
        }
        for (int i = items.Length-1; i > -1; i--,cnt++)
        {
            if (i == id)
                return cnt;
        }
        return id;
    }

    private void StartMove(int dif, bool isNext) 
    {
        foreach(CarouselItem item in items)
        {
            item.SwitchToClicked(dif,isNext);
        }
    }
}
