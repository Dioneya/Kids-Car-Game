using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void EatDonut()
    {

        animator.Play("Interactive",0);
    }

    public void Idle()
    {
        animator.Play("Idle", 0);
    }

    public void Walk()
    {
        animator.Play("Wallk", 0);
    }
    public void Swit()
    {
        animator.Play("Swit",0);
    }
}
