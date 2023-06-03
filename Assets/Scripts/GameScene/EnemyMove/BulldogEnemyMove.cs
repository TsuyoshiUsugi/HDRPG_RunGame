using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���i����G�̈ړ��X�N���v�g
/// </summary>
public class BulldogEnemyMove : MonoBehaviour, IEnemyMove
{
    Vector3 _dir = Vector3.back;

    public void EnemyMove(float speed)
    {
        this.transform.position += _dir * speed * Time.deltaTime;
    }
}
