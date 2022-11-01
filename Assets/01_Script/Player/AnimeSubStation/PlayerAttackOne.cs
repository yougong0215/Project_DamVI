using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAttackOne : PlayerAttackBase
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

    public override IEnumerator OndamagedEnemyRangeAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    public override void OnDamagedEnemyMelloAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, Collider[] col)
    {

    }

    public override void OnDamageEffectStart(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //test code
        
    }

    public override void OnDamageEffectHold(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnDamageEffectEnd(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
