using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��莞�Ԃ��Ƃɉ����v���C���[�Ɍ������Ĕ��˂���
/// </summary>
public class BossAttack1_1 : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject _flame;
    [SerializeField] float _dur = 3;
    [SerializeField] float _currentTime = 0;

    public void EnemyAttack()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dur)
        {
            Instantiate(_flame, transform.position, transform.rotation);
            _currentTime = 0;
        }

    }
}
