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
    [SerializeField] Vector3 _speed = new Vector3(1, 0, 0);
    float _leftSide = 0;
    float _rightSide = 0;

    private void Start()
    {
        _leftSide = GameSceneManager.Instance.GetFieldInfo().leftSide;
        _rightSide = GameSceneManager.Instance.GetFieldInfo().rightSide;
    }

    public void EnemyMove(float speed)
    {
        transform.position += _speed * Time.deltaTime;

        if (transform.position.x <= _leftSide) _speed = -_speed;
        if (transform.position.x >= _rightSide) _speed = -_speed;
    }
}
