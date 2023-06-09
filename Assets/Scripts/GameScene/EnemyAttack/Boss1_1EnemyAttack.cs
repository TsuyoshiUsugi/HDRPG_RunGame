using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定時間ごとに炎をプレイヤーに向かって発射する
/// </summary>
public class BossAttack1_1 : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject _flame;
    [SerializeField] float _dur = 3;
    float _currentTime = 0;

    public void EnemyAttack()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dur)
        {
            Instantiate(_flame);
            _currentTime = 0;
        }

    }
}
