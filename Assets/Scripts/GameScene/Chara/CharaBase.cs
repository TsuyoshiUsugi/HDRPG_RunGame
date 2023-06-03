using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵、Player、ボスのベースとなるクラス
/// 
/// [変数]
/// ・HP
/// ・ATK
/// ・Speed
/// ・ATKRate
/// 
/// [関数]
/// ・攻撃
/// ・移動
/// 
/// [interface]
/// ・ヒット
/// ・ポーズ
/// </summary>
public class CharaBase : MonoBehaviour, IHit, IPosable
{
    [Header("設定値")]
    [SerializeField] int _hp = 1;
    [SerializeField] int _atk = 1;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] float _atkRate = 1;
    
    BoxCollider _range;
    protected bool _isPose = false;
    public BoolReactiveProperty IsDeath = new BoolReactiveProperty();

    protected virtual void Attack() { }

    protected virtual void AutoForwardMove() { }

    public void Hit(int damage)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out Player player);
        if ( player != null) player.Hit(_atk);
    }

    public void Pose(bool isPoseing)
    {
        _isPose = isPoseing;
    }
}
