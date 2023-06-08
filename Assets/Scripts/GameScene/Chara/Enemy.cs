using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���V�[���̃��u�G�l�~�[�̃X�N���v�g
/// ���Ă���IEnemyMove�R���|�[�l���g���擾���A����ɍ��킹������������
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
