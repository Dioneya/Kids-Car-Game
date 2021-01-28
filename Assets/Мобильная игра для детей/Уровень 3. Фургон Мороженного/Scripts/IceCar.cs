using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IceCar : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject[] iceCreams;
    [SerializeField] GameObject[] slots;
    public static UnityEvent feedChild;
    public static UnityEvent endFeed;
    void Start()
    {
        if (feedChild == null)
            feedChild = new UnityEvent();
        if (endFeed == null)
            endFeed = new UnityEvent();
        feedChild.AddListener(ShowIceCream);
        endFeed.AddListener(CloseIceCream);
    }

    void CloseIceCream()
    {
        foreach (GameObject i in iceCreams)
            i.GetComponent<IceCream>().Hide();

        foreach (GameObject i in slots)
            i.SetActive(false);
    }

    void ShowIceCream()
    {
        foreach (GameObject i in iceCreams)
            i.GetComponent<IceCream>().Show();

        foreach (GameObject i in slots)
            i.SetActive(true);
    }

    void OnDestroy() 
    {
        feedChild.RemoveAllListeners();
        endFeed.RemoveAllListeners();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.name.Contains("IceCreamBox"))
        {
            IceBar.addValue.Invoke();
            eventData.pointerDrag.gameObject.SetActive(false);
            //eventData.pointerDrag.GetComponent<Glowing>().AddGlow();
        }
    }
}
