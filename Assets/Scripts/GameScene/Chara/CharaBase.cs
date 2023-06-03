using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵、Player、ボスのベースとなるクラス
/// </summary>
public class CharaBase : MonoBehaviour, IHit
{
    [Header("設定値")]
    [SerializeField] int _hp = 1;
    [SerializeField] int _atk = 1;
    [SerializeField] protected float _speed = 0.1f;
    [SerializeField] float _atkRate = 1;
    
    BoxCollider _range;
    public BoolReactiveProperty IsDeath = new BoolReactiveProperty();

    protected virtual void Attack() { }

    protected virtual void Move() { }

    public void Hit(int damage)
    {
        
    }
}
