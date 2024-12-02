using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : StateMachineBehaviour
{
    //상태에 들어왔을 때 호출
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDelay", true);
    }

    //상태에서 빠져나갈 때 호출
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDelay", false);
    }

}
