using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    private Vector3 targetPos;
    private Vector3 direction;
    private SpriteRenderer sprite;
    public bool isMoving = false;
    private bool isLess = false;
    [SerializeField] private bool isMenu = false;
    [SerializeField] private bool isFliped;
    Vector3 previousPos;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!isMoving)
            return;
        if(isLess)
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        else
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        if (isLess && transform.position.x <= targetPos.x)
        {
            Stop();
            if(isMenu) GoTo(previousPos);   
        }   
        else if (!isLess && transform.position.x >= targetPos.x) 
        {
            Stop();
            if(isMenu) GoTo(previousPos);
        }
    }
    public void Stop()
    {
        isMoving = false;
    }
    public void GoTo(Vector3 pos)
    {
        if(isMenu)
            previousPos = transform.position;
        Debug.LogWarning(pos + " " + transform.position);
        isMoving = true;
        targetPos = pos;
        if (transform.position.x > pos.x)
        {
            sprite.flipX = isFliped;
            isLess = true;
        }

        if (transform.position.x < pos.x)
        {
            sprite.flipX = !isFliped;
            isLess = false;
        }

        var dir = (pos - transform.position).normalized;
        direction = new Vector3(dir.x, 0, 0);
    }
}
