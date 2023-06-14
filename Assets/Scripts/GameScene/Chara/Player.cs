using UnityEngine;
using System;

public class Player : CharaBase
{
    [SerializeField] int _level = 1;
    float _goalPosZ = 10;
    Vector3 _moveDir = Vector3.forward;

    string _attackSeName = "Attack";
    string _hitSeName = "Hit";

    public event Action<float> OnPlayerHitEvent;

    private void Start()
    {
        _goalPosZ = GameSceneManager.Instance.Goal;
    }

    public void Initialize(int level, int hp, int atk, float speed, float atkRate, Vector3 attackHitBoxPos, Vector3 attackHitBoxSize)
    {
        _level = level;
        _hp.Value = hp;
        _atk = atk;
        _speed = speed;
        _atkRate = atkRate;
        _attackHitBox.transform.localPosition = attackHitBoxPos;
        _attackHitBox.transform.localScale = attackHitBoxSize;
    }

    /// <summary>
    /// 勝手に前に進む処理
    /// </summary>
    protected override void AutoForwardMove()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;

        if (transform.position.z > _goalPosZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _goalPosZ);
        }
    }

    /// <summary>
    /// Playerの入力により左右に進む処理
    /// </summary>
    /// <param name="isLeft">trueなら左、falseなら右</param>
    public void LeftRightMove(bool isLeft)
    {
        if (_isPose.Value) return;
        if (isLeft) transform.position += Vector3.left * _speed * Time.deltaTime;
        if (!isLeft) transform.position -= Vector3.left * _speed * Time.deltaTime;

        ResetPos();
    }

    public new async void Hit(int damage)
    {
        if (GameSceneManager.Instance.IsClear) return;
        if (_isHit) return;

        _hp.Value -= damage;
        if (damage <= 0) return;

        PlaySE(_hitSeName);

        if (_hp.Value <= 0)
        {
            _hp.Value = 0;
            _isDeath.Value = true;
        }


        _isHit = true;
        OnPlayerHitEvent(_flashDur * _flashTime);
        await ShowHit();
        _isHit = false;

    }

    public override void Attack()
    {
        if (_isPose.Value) return;
        if (_hitBox == null) return;
        
        PlaySE(_attackSeName);

        foreach (var col in _hitBox.HitObjs)
        {
            if (col == null) continue;
            col.TryGetComponent(out Enemy enemy);
            if (enemy) enemy.Hit(_atk);
        }
    }
}
