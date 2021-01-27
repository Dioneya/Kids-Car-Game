using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kid : MonoBehaviour
{
    private bool isMove = false;
    private Vector3 coordToMove = new Vector3();
    private Animator animator;

    void Awake() 
    {
        animator = GetComponent<Animator>(); 
    }
    void Start()
    {
        
    }

    public void MoveTo(Vector3 toMove)
    {
        isMove = true;
        animator.Play("Run");
        coordToMove = toMove;
    }
    void Update()
    {
        if (isMove)
            transform.position += Vector3.right * 3f * Time.deltaTime;
        if (transform.position.x >= coordToMove.x)
        {
            isMove = false;
            animator.Play("Idle");
        }
    }
}
