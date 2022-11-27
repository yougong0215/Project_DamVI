using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBUgSolce : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.hit;

    }
}
