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
    [Header("設定値")]
    [SerializeField] float _speed = 1;
    [SerializeField] BossState _currentState = BossState.Float;
    [SerializeField] float _ground = 0;
    float _leftSide = 0;
    float _rightSide = 0;
    Vector3 _horizontalVector; 
    Vector3 _verticalVector; 

    private void Start()
    {
        _currentState = BossState.Float;
        _horizontalVector = new Vector3(_speed, 0, 0);
        _verticalVector = new Vector3(0, _speed, 0);
        _leftSide = GameSceneManager.Instance.GetFieldInfo().leftSide;
        _rightSide = GameSceneManager.Instance.GetFieldInfo().rightSide;
    }

    public void EnemyMove(float speed)
    {
        if (_currentState == BossState.Float)
        {
            transform.position += _horizontalVector * Time.deltaTime;

            if (transform.position.x <= _leftSide) _horizontalVector = -_horizontalVector;
            if (transform.position.x >= _rightSide) _horizontalVector = -_horizontalVector;
        }
        else if (_currentState == BossState.Rest)
        {
            transform.position -= _verticalVector * Time.deltaTime;

            if (transform.position.y <= _ground) transform.position = 
                    new Vector3(transform.position.x, _ground, transform.position.z);
        }
    }

    enum BossState
    {
        Float,
        Rest,
    }
}
