using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

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
    [SerializeField] float _switchStateTime = 10;

    float _originPos = 0;
    [SerializeField] float _currentTime = 0;
    float _leftSide = 0;
    float _rightSide = 0;
    Vector3 _horizontalVector; 
    Vector3 _verticalVector;

    private void Start()
    {
        _originPos = transform.position.y;
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
            FloatState();

        }
        else if (_currentState == BossState.Rest)
        {
            RestState();
        }
    }

    private void FloatState()
    {
        if (_currentTime <= _switchStateTime)
        {
            _currentTime += Time.deltaTime;

            transform.position += _horizontalVector * Time.deltaTime;

            if (transform.position.x <= _leftSide) _horizontalVector = -_horizontalVector;
            if (transform.position.x >= _rightSide) _horizontalVector = -_horizontalVector;
        }
        else
        {
            _currentTime = 0;
            _currentState = BossState.Rest;
        }
    }

    /// <summary>
    /// 休息時の動きの関数
    /// </summary>
    private void RestState()
    {

        if (_currentTime <= _switchStateTime)
        {
            _currentTime += Time.deltaTime;
            transform.position -= _verticalVector * Time.deltaTime;

            if (transform.position.y <= _ground) transform.position =
                    new Vector3(transform.position.x, _ground, transform.position.z);
        }
        else
        {
            transform.position += _verticalVector * Time.deltaTime;

            if (transform.position.y >= _originPos)
            {
                transform.position = new Vector3(transform.position.x, _originPos, transform.position.z);
                _currentTime = 0;
                _currentState = BossState.Float;
            }
        }
    }

    enum BossState
    {
        Float,
        Rest,
    }
}
