using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : StateMachineBehaviour
{
    GunMinater _g;

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
        
        _g = animator.GetComponent<GunMinater>();
        animator.SetBool("Attack", false);
        _g._rotate = true;
        shot = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(shot == false)
        Player.GetComponent<MonoBehaviour>().StartCoroutine(Shoot(animator));
    }

    IEnumerator Shoot(Animator animator)
    {

        //dir = (Player.position + new Vector3(0, 1.4f, 0) - transform.position).normalized;
        
        shot = true;
        yield return new  WaitForSeconds(0.05f);
        _g.Att(15);
        yield return null;
        _g.Att(12);
        yield return null;
        _g.Att(9);
        yield return null;
        _g.Att(6);
        yield return null;
        _g.Att(3);
        yield return new WaitForSeconds(0.1f);
        _g.Att();
        yield return new WaitForSeconds(0.1f);
        _g.Att(-3);
        yield return null;
        _g.Att(-6);
        yield return null;
        _g.Att(-9);
        yield return null;
        _g.Att(-12);
        yield return null;
        _g.Att(-15);
        _g._rotate = false;
    }
}
