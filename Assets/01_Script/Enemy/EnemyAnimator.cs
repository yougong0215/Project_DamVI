using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : StateMachineBehaviour
{
    private Transform _player;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
            return _player;
        }
    }
    bool shot = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shot = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(shot == false)
        Player.GetComponent<MonoBehaviour>().StartCoroutine(Shoot(animator));
    }

    IEnumerator Shoot(Animator animator)
    {
        shot = true;
        yield return new  WaitForSeconds(0.05f);
        animator.GetComponent<GunMinater>().Att();
        yield return new WaitForSeconds(0.05f);
        animator.GetComponent<GunMinater>().Att();
        yield return new WaitForSeconds(0.05f);
        animator.GetComponent<GunMinater>().Att();
    }
}
