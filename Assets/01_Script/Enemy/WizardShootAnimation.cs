using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShootAnimation : StateMachineBehaviour
{
    AIWizard _wiz;

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

        _wiz = animator.GetComponent<AIWizard>();
        animator.SetBool("Attack", false);
        _wiz._rotate = true;
        shot = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shot == false)
            Player.GetComponent<MonoBehaviour>().StartCoroutine(Shoot(animator));
    }

    IEnumerator Shoot(Animator animator)
    {

        //dir = (Player.position + new Vector3(0, 1.4f, 0) - transform.position).normalized;
        yield return new WaitForSeconds(0.05f);
        shot = true;
        animator.speed = 0.3f;
        yield return new WaitForSeconds(0.5f);
        animator.speed = 1;

        shot = true;
        yield return new WaitForSeconds(0.05f);

        _wiz.Att(2);
        animator.GetComponent<AudioSource>().PlayOneShot(animator.GetComponent<EnemyBase>().audios);
        yield return null;
        _wiz.Att();
        yield return null;
        _wiz.Att(-2);

        _wiz._rotate = false;
    }
}
