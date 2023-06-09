using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1-1で出現するボスの動きのスクリプト
/// 左右に移動して一定時間後,地面に落下
/// これを繰り返す
/// </summary>
public class Boss1_1Move : MonoBehaviour, IEnemyMove
{
    [SerializeField] Vector3 _speed = new Vector3(10, 0, 0);

    public void EnemyMove(float speed)
    {
        this.transform.position -= _speed;
    }
}
