using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimeAttack : StateMachineBehaviour
{
    AIDogNight ai;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<AIDogNight>();
        ai._rotate = false;
        animator.speed = 0.1f;
        animator.GetComponent<MonoBehaviour>().StartCoroutine(Attack(animator));
    }
    Collider[] col;
    [SerializeField] LayerMask layer;

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



    IEnumerator Attack(Animator ani)
    {
        yield return new WaitForSeconds(1f);
        
        ani.speed = 1;
        col = Physics.OverlapBox(ani.transform.position + ani.transform.forward
        , (Player.position - ani.transform.position).normalized + new Vector3(1.2f, 1.2f, 1.2f), ani.transform.rotation
        , layer);


        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].GetComponent<PlayerInteraction>())
            {
                col[i].GetComponent<PlayerInteraction>().Damaged(ai.ATK);
                ai._AttackDelayTime = 1.4f;
                ai._rotate = true;
                Player.GetComponent<MonoBehaviour>().StopAllCoroutines();
                
            }
        }
        ani.GetComponent<AudioSource>().PlayOneShot(ani.GetComponent<EnemyBase>().audios);
        ai._AttackDelayTime = 0.8f;

    }
    

}
