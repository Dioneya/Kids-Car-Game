using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 mousePos;
    private int _paddingMin =40;
    private int _paddingMax = 80;
    public bool isPressed()
    {
        if(Input.GetMouseButton(0) || Input.touchCount >0 && Input.touchCount !=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        Input.multiTouchEnabled = false;
        MoveBorder();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Move(Input.mousePosition);
        }
        if (Input.touchCount >0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Move(Input.GetTouch(0).position);
            }
        }
    }
    private void Move(Vector3 position)
    {

        var newPosX = Mathf.Clamp(position.x, xMin, xMax);
        var newPosY = Mathf.Clamp(position.y, yMin, yMax);
        mousePos = new Vector2(newPosX, newPosY);
    }

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    private void MoveBorder()
    {
#if UNITY_EDITOR
        xMin = _paddingMin;
        xMax = Screen.width - _paddingMin;
        yMin = _paddingMin;
        yMax = Screen.height - _paddingMin;
#endif
#if UNITY_IOS && !UNITY_ANDROID && !UNITY_EDITOR
        yMin = _paddingMin;
        xMin = _paddingMin;
        xMax = Screen.width - _paddingMin;
        yMax = Screen.height - _paddingMax;
#endif
#if UNITY_ANDROID && !UNITY_IOS && !UNITY_EDITOR

        yMin = _paddingMin;
        xMin = _paddingMin;
        xMax = Screen.width - _paddingMax;
        yMax = Screen.height - _paddingMin;
#endif
    }

}
