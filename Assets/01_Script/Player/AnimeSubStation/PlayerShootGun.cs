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
        //for (int i = -3; i <= 3; i += 2)
        {
            for(int j = -3; j <= 3; j += 2)
            {
                Player.GetComponent<PlayerMove>().LookObject.GetComponent<CameraCollision>().shaking(0.1f, 0.5f, 1);
                Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.ActivityBullet, j * 3, j * 3, true);
                yield return null;
            }

            //yield return null;
            for (int j = 3; j >= -3; j -= 2)
            {
                Player.GetComponent<PlayerMove>().LookObject.GetComponent<CameraCollision>().shaking(0.1f, 0.5f, 1);
                Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.ActivityBullet, j * 3, j * 3, true);
                yield return null;
            }

            for (int j = 3; j >= -3; j -= 2)
            {
                Player.GetComponent<PlayerMove>().LookObject.GetComponent<CameraCollision>().shaking(0.1f, 0.5f, 1);
                Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.ActivityBullet, 0, j * 3, true);
                yield return null;
            }
            for (int j = 3; j >= -3; j -= 2)
            {
                Player.GetComponent<PlayerMove>().LookObject.GetComponent<CameraCollision>().shaking(0.1f, 0.5f, 1);
                Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.ActivityBullet,  j * 3, 0, true);
                yield return null;
            }
        }
        Debug.Log("¼¦");
    }

}
