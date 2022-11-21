using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackThree : PlayerAttackBase
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
        Player.GetComponent<MonoBehaviour>().StartCoroutine(ShootCool(3));
        
    }

    IEnumerator ShootCool(int count)
    {
        yield return new WaitForSeconds(0.05f);
        Player.GetComponent<Rigidbody>().velocity = Player.forward * 1 * 2;
        yield return new WaitForSeconds(0.05f);
        LookEnemy();
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Weapon>().fire(WeaponType.Right, BulletType.Bulletbase);
        if(count > 0)
        {
            Player.GetComponent<MonoBehaviour>().StartCoroutine(ShootCool(count - 1));
        }
        
    }

}
