using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : StateMachineBehaviour
{
    private Transform _player;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.Find("Player").GetComponent<Transform>();
            }
            return _player;
        }
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetComponent<MonoBehaviour>().StartCoroutine(Die(animator));
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.die;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.die;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.die;
        
    }
    IEnumerator Die(Animator animator)
    {
        yield return new WaitForSeconds(1);
        animator.speed = 0;
        
    }

}
