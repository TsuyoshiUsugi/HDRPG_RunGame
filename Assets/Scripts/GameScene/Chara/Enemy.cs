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
    [SerializeField] int _score = 1;
    [SerializeField] int _exp = 1;

    readonly Vector3 _grave = new Vector3(1000, 1000, 1000);
    IEnemyMove _enemyMove;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
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

        if (_hp <= 0)
        {
            _hp = 0;
            IsDeath.Value = true;
            GameSceneManager.Instance.AddScore(_score);
            GameSceneManager.Instance.AddExp(_exp);

            this.enabled = false;
        }
    }
}
