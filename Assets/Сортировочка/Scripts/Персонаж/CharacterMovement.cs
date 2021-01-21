using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public event Action OnTargetReached;

    [SerializeField] float movementSpeed;
    [SerializeField] float reachDist = 1;
    private Vector3 targetPos;
    private Vector3 direction;
    private SpriteRenderer sprite;
    private bool isMoving = true;
    void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!isMoving)
            return;

        if(Mathf.Abs(targetPos.x - transform.position.x) <= 1)
        {
            OnTargetReached?.Invoke();
        }

        transform.position += direction * movementSpeed * Time.deltaTime;
    }
    public void Stop()
    {
        isMoving = false;
    }
    public void GoTo(Vector3 pos)
    {
        Debug.LogWarning(pos + " " + transform.position);
        isMoving = true;
        targetPos = pos;
        if(transform.position.x > pos.x)
        {
            sprite.flipX = false;
        }

        if (transform.position.x < pos.x)
        {
            sprite.flipX = true;
        }

        var dir = (pos - transform.position).normalized;
        direction = new Vector3(dir.x,0,0);
    }
}
