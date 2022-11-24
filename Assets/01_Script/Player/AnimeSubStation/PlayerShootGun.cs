using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootGun : PlayerAttackBase
{
    public override void OnDamageEffectStart(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Weapon", false);
    }
    public override IEnumerator OndamagedEnemyRangeAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, float delay)
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = -60; i <= 60; i += 20)
        {
            for(int j = -60; j <=60; j+= 20)
            {
               
                Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.RedBullet, i, j);
            }
        }
    }


    public override void OnDamageEffectHold(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.Play("Weapon");
        }
    }
}
