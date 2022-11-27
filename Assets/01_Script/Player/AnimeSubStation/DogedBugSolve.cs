using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogedBugSolve : StateMachineBehaviour
{
    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.SetBool("Doged", false);
    }
}
