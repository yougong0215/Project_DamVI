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
        for (int i = -6; i <= 6; i += (int)Random.Range(1f,4f))
        {
            for(int j = -6; j <= 6; j += (int)Random.Range(1f, 4f))
            {
                Player.GetComponent<PlayerMove>().LookObject.GetComponent<CameraCollision>().shaking(0.1f, 0.5f, 1);
                Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.ActivityBullet, i, j, true);
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
