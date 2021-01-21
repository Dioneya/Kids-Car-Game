using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDarking : MonoBehaviour
{
    Image image;
    void Start() 
    {
        Dark();
    }
    public void Dark()
    {
        StartCoroutine(SmoothDark());
    }

    public void Remove()
    {
        image.enabled = false;
    }

    private IEnumerator SmoothDark()
    {
        image = GetComponent<Image>();

        for (float i = 0; i != 1; i+= 0.02f)
        {
            var c = image.color;
            c.a = i;
            image.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
