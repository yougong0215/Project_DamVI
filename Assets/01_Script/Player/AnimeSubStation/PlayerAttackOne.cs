using System.Collections;
using UnityEngine;

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

    public override IEnumerator OndamagedEnemyRangeAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, float delay)
    {
        LookEnemy();
        Player.GetComponent<Rigidbody>().velocity = Player.forward * 1 * 30;
        yield return new WaitForSeconds(delay);
        Player.GetComponent<Weapon>().fire(WeaponType.Left, BulletType.RedBullet);
    }


    public IEnumerator late()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void OnDamagedEnemyMelloAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, Collider[] col)
    {

    }

    public override void OnDamageEffectStart(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //test code
        Player.GetComponent<MonoBehaviour>().StartCoroutine(late());
    }

    public override void OnDamageEffectHold(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnDamageEffectEnd(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
