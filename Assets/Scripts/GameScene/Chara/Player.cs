using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : CharaBase
{
    Vector3 _moveDir = Vector3.forward;

    // Update is called once per frame
    void Update()
    {
        if (_isPose) return;

        AutoForwardMove();
    }

    /// <summary>
    /// ����ɑO�ɐi�ޏ���
    /// </summary>
    protected override void AutoForwardMove()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    /// <summary>
    /// Player�̓��͂ɂ�荶�E�ɐi�ޏ���
    /// </summary>
    /// <param name="isLeft">true�Ȃ獶�Afalse�Ȃ�E</param>
    public void LeftRightMove(bool isLeft)
    {
        if (_isPose) return;
        if (isLeft) transform.position += Vector3.left * _speed * Time.deltaTime;
        if (!isLeft) transform.position -= Vector3.left * _speed * Time.deltaTime;

        ResetPos();
    }

    public void Hit(int damage)
    {
        Debug.Log("Hit");
        _hp -= damage;

        if (_hp <= 0)
        {
            _hp = 0;
            IsDeath.Value = true;
        }
    }
}
