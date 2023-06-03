using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : CharaBase
{
    Vector3 _moveDir = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {

    }

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
    }

    public void Hit(int damage)
    {

    }

}
