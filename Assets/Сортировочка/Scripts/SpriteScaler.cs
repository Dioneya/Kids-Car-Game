using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    [SerializeField] bool isCorrectingPosition;

    const float defaultHeight = 1080f;
    const float defaultWidth = 1920f;
    const float defaultProportion = defaultWidth / defaultHeight;


    void Awake()
    {
        Debug.Log(transform.localScale.x +" "+ defaultWidth + " " + Screen.width);
        var proportion = 1f * Screen.width / Screen.height;

        var x = transform.localScale.x / defaultProportion * proportion;
        var y = transform.localScale.y / defaultProportion * proportion;
        var oldScale = transform.localScale;

        transform.localScale = new Vector3(x,y,0);

        if (isCorrectingPosition)
        {
            var deltaX = oldScale.x - x;
            var deltaY = oldScale.y - y;
            Debug.Log(deltaX + " " + deltaY);

            transform.position += new Vector3(deltaX * 4.5f,-deltaY * 3,0);
        }
        else
        {
            //transform.position += Vector3.down * defaultProportion / proportion;
        }
    }

    
}
