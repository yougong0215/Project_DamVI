using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTwo : PlayerAttackBase
{
    //////////////////////////////////////////////////////////////////
    // 이 스크립트들 붙은 에니메이션 가보면 List -> Effect 넣는란 있음
    // GameObject 형태인데 문제있으면 바꿔달라하셈
    // 만약 이거 안쓰고 메니저 만들어서 관리하면 지우라고 말좀해줘
    //
    // OnDamageEffectStart 에니메이션 첫 프레임때 호출
    // OnDamageEffectHold 애니메이션 Update 같은 느낌
    // OnDamageEffectEnd 애니메이션 끝 프레임 ( 안 쓸 가능성 농후 하긴함 )
    //
    // 인스팩터에서 size 정해주샘 기본값 ( 1.5, 1.5, 1.5 )
    // 공격범위 만큼 넣어주면됨
    //
    // 인스팩터에서 enum 관련된거 건들 ㄴㄴ
    //
    //               해줘야 될것
    // 
    // 이팩트 넣어주기
    // 에니메이션 확인하고 이팩트 각도 맞춰주기 
    // 공격에 맞게 공격 범위 설정해주기
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
