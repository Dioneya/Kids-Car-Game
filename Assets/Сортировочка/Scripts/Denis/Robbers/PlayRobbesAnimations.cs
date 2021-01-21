using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRobbesAnimations 
{
    private Animator _animator;
    private readonly string _keyAnimationIdle = "Idle";
    private readonly string _keyAnimationHandcuffed = "Handcuffed";

    public PlayRobbesAnimations(Animator animator)
    {
        _animator = animator;
    }
    public void PlayIdleAnimation()
    {
        _animator.SetBool(_keyAnimationIdle, true);
        _animator.SetBool(_keyAnimationHandcuffed, false);
    }
    public void PlayHandcuffedAnimation()
    {
        _animator.SetBool(_keyAnimationIdle, false);
        _animator.SetBool(_keyAnimationHandcuffed, true);
    }
}
