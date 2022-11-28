using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackFive : PlayerAttackBase
{
    //////////////////////////////////////////////////////////////////
    // �� ��ũ��Ʈ�� ���� ���ϸ��̼� ������ List -> Effect �ִ¶� ����
    // GameObject �����ε� ���������� �ٲ�޶��ϼ�
    // ���� �̰� �Ⱦ��� �޴��� ���� �����ϸ� ������ ��������
    //
    // OnDamageEffectStart ���ϸ��̼� ù �����Ӷ� ȣ��
    // OnDamageEffectHold �ִϸ��̼� Update ���� ����
    // OnDamageEffectEnd �ִϸ��̼� �� ������ ( �� �� ���ɼ� ���� �ϱ��� )
    //
    // �ν����Ϳ��� size �����ֻ� �⺻�� ( 1.5, 1.5, 1.5 )
    // ���ݹ��� ��ŭ �־��ָ��
    //
    // �ν����Ϳ��� enum ���õȰ� �ǵ� ����
    //
    //               ����� �ɰ�
    // 
    // ����Ʈ �־��ֱ�
    // ���ϸ��̼� Ȯ���ϰ� ����Ʈ ���� �����ֱ� 
    // ���ݿ� �°� ���� ���� �������ֱ�
    // 
    //
    //////////////////////////////////////////////////////////////////

    public override IEnumerator OndamagedEnemyRangeAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, float delay)
    {
        yield return null;
        if (PlayerAttackManager.Instance.Bullet.CanUse() == 1)
        {
            Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.ActivityBullet);
            PlayerAttackManager.Instance.Bullet.UseingShoot();
        }
        else if(PlayerAttackManager.Instance.Bullet.CanUse() == 0)
        {
            PlayerAttackManager.Instance.Bullet.UseingShoot();
            Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.Bulletbase);
        }
        else
        {
            Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.Bulletbase);
        }

        //Player.GetComponent<PlayerMove>().CameraReturn().shaking(0.05f, 0.05f);
    }


}
