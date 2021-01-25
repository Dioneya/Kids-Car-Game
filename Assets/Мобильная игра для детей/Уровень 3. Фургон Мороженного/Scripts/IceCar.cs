using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IceCar : MonoBehaviour, IDropHandler
{
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
