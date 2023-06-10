using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

/// <summary>
/// ゲームシーンのモブエネミーのスクリプト
/// ついているIEnemyMoveコンポーネントを取得し、それに合わせた動きをする
/// </summary>
public class Enemy : CharaBase
{
    [Header("設定値")]
    [SerializeField] int _score = 1;
    [SerializeField] int _exp = 1;

    IEnemyMove _enemyMove;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        TryGetComponent<IEnemyMove>(out _enemyMove);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_isPose) return;
        AutoForwardMove();
        ResetPos();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hp <= 0) return;

        other.TryGetComponent<Player>(out Player player);
        if (player != null) player.Hit(_atk);
    }

    protected override void AutoForwardMove()
    {
        if (_enemyMove != null) _enemyMove.EnemyMove(_speed);
    }

    public new void Hit(int damage)
    {
        if (_hp <= 0) return;

        Debug.Log("Hit");

        ShowHit();
        _hp -= damage;
        Death();
    }

    protected virtual void Death()
    {
        if (_hp <= 0)
        {
            _hp = 0;
            GameSceneManager.Instance.AddScore(_score);
            GameSceneManager.Instance.AddExp(_exp);
            IsDeath.Value = true;
            this.enabled = false;
            
        }
    }
}
