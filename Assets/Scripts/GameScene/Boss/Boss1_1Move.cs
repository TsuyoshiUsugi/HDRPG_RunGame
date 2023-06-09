using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1-1�ŏo������{�X�̓����̃X�N���v�g
/// ���E�Ɉړ����Ĉ�莞�Ԍ�,�n�ʂɗ���
/// ������J��Ԃ�
/// </summary>
public class Boss1_1Move : MonoBehaviour, IEnemyMove
{
    [SerializeField] Vector3 _speed = new Vector3(10, 0, 0);

    public void EnemyMove(float speed)
    {
        this.transform.position -= _speed;
    }
}
