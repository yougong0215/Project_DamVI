

/// <summary>
/// 1 - ������
/// 2 - �ȱ�
/// 3 - �ٱ�
/// 8 - ȸ��
/// 10 - �������
/// 11~15 - �⺻����
/// 17~18 - �б����
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
    /// �Ϲ� ī�޶�
    /// </summary>
    Idle = 0,
    Walk = 3,
    Move = 5,

    evasion = 8, 

    /// <summary>
    /// 10���� ī�޶�
    /// </summary>
    NormalAttack1 = 11,
    NormalAttack2 = 12,
    NormalAttack3 = 13,
    NormalAttack4 = 14,
    NormalAttack5 = 15,

    bifurcationAttack1 = 17,
    bifurcationAttack2 = 18,
    /// <summary>
    /// ���� ī�޶�
    /// </summary>
    EquidAttack = 20,
}