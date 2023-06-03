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
    /// 勝手に前に進む処理
    /// </summary>
    protected override void AutoForwardMove()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    /// <summary>
    /// Playerの入力により左右に進む処理
    /// </summary>
    /// <param name="isLeft">trueなら左、falseなら右</param>
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
