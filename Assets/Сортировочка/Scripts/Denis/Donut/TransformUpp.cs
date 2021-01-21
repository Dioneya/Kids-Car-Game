using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUpp : MonoBehaviour
{
    private float _speed = 2;
    private void Start()
    {
        StartCoroutine(MoveNumbers());
    }
    public IEnumerator MoveNumbers()
    {
        while (transform.localPosition.y != Screen.height / 2)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(Screen.height / 2, Screen.width), _speed * Time.deltaTime);
            yield return null;
        }
    }
}
