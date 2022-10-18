

/// <summary>
/// 1 - 대기상태
/// 2 - 걷기
/// 3 - 뛰기
/// 8 - 회피
/// 10 - 무기공격
/// 11~15 - 기본공격
/// 17~18 - 분기공격
/// </summary>
/// 

public enum PlayerPripoty
{
    none = 0,
    Move = 1,
    Fight = 2,
    hit = 3,
    aiming = 4,
    evasion = 5,
    doged = 6
}
public enum PlayerStatues
{
    /// <summary>
    /// 일반 카메라
    /// </summary>
    Idle = 0,
    Walk = 3,
    Move = 5,

    evasion = 8, 

    /// <summary>
    /// 10번대 카메라
    /// </summary>
    NormalAttack1 = 11,
    NormalAttack2 = 12,
    NormalAttack3 = 13,
    NormalAttack4 = 14,
    NormalAttack5 = 15,

    bifurcationAttack1 = 17,
    bifurcationAttack2 = 18,
    /// <summary>
    /// 조준 카메라
    /// </summary>
    EquidAttack = 20,
}