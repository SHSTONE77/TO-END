using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;
    void Start(){
        animator = gameObject.GetComponent<Animator>();
    }

    public void endDash(){
        animator.SetBool("isTele", false);
    }
}
