using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシーンのモブエネミーのスクリプト
/// ついているIEnemyMoveコンポーネントを取得し、それに合わせた動きをする
/// </summary>
public class Enemy : CharaBase
{
    IEnemyMove _enemyMove;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<IEnemyMove>(out _enemyMove);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPose) return;
        AutoForwardMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out Player player);
        if (player != null) player.Hit(_atk);
    }

    protected override void AutoForwardMove()
    {
        if (_enemyMove != null) _enemyMove.EnemyMove(_speed);
    }
}
