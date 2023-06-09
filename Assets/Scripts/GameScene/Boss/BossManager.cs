using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスの行動を管理するクラス
/// </summary>
public class BossManager : Enemy
{
    IEnemyAttack _enemyAttack;

    private new void Start()
    {
        base.Start();
        TryGetComponent<IEnemyAttack>(out _enemyAttack);
    }

    private new void Update()
    {
        base.Update();

        Attack();
    }

    public override void Attack()
    {
        if (_enemyAttack != null) _enemyAttack.EnemyAttack();
    }

    protected override void Death()
    {
        base.Death();
    }
}
