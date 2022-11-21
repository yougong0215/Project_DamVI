using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackFour : PlayerAttackBase
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
        yield return new WaitForSeconds(delay);
        Player.GetComponent<MonoBehaviour>().StartCoroutine(ShootCool(3, WeaponType.Left, BulletType.Bulletbase));
        yield return new WaitForSeconds(0.3f);
        Player.GetComponent<MonoBehaviour>().StartCoroutine(ShootCool(3, WeaponType.Right, BulletType.RedBullet));
        yield return new WaitForSeconds(0.9f);
        Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.RedBullet);
        Player.GetComponent<PlayerMove>().CameraReturn().shaking(0.1f, 0.2f);
    }

    IEnumerator ShootCool(int count, WeaponType wea, BulletType bul)
    {
        yield return new WaitForSeconds(0.05f);
        Player.GetComponent<Rigidbody>().velocity = Player.forward * 1 * 2;
        yield return new WaitForSeconds(0.05f);
        LookEnemy();
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Weapon>().fire(wea, bul);
        if (count > 0)
        {
            Player.GetComponent<MonoBehaviour>().StartCoroutine(ShootCool(count - 1, wea, bul));
        }

    }
}
