using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTwo : PlayerAttackBase
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

    public override void OnDamageEffectStart(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnDamageEffectStart(animator, stateInfo, layerIndex);
        Player.GetComponent<MonoBehaviour>().StartCoroutine(late());
    }
    public override IEnumerator OndamagedEnemyRangeAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, float delay)
    {
        Player.GetComponent<Rigidbody>().velocity = Player.forward * -1 * 30;
        yield return new WaitForSeconds(delay);
        LookEnemy();
        Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.Bulletbase);
        Player.GetComponent<PlayerMove>().CameraReturn().shaking(0.1f, 0.1f);
    }


    public IEnumerator late()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
