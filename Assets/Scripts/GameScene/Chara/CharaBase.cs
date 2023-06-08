using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using System;

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
/// ・Hit
/// ・位置修正
/// 
/// [interface]
/// ・ヒット
/// ・ポーズ
/// </summary>
public class CharaBase : MonoBehaviour, IHit, IPosable
{
    [Header("設定値")]
    [SerializeField] protected int _hp = 1;
    [SerializeField] protected int _atk = 1;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] float _atkRate = 1;
    [SerializeField] protected float _leftLimit = -2.5f;
    [SerializeField] protected float _rightLimit = 2;
    int _flashTime = 2;
    float _flashDur = 0.1f;
    
    SpriteRenderer _spriteRenderer;
    protected bool _isPose = false;
    public BoolReactiveProperty IsDeath = new BoolReactiveProperty();

    protected void Start()
    {
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
    }

    public virtual void Attack() { }

    protected virtual void AutoForwardMove() { }

    public void Hit(int damage) { }

    public void Pose(bool isPoseing)
    {
        _isPose = isPoseing;
    }

    /// <summary>
    /// 設定されている領域外にでないようにする
    /// </summary>
    protected void ResetPos()
    {
        if (transform.position.x > _rightLimit) transform.position = new Vector3(_rightLimit, transform.position.y, transform.position.z);
        if (transform.position.x < _leftLimit) transform.position = new Vector3(_leftLimit, transform.position.y, transform.position.z);
    }

    private void OnEnable()
    {
        GameSceneManager.Instance.PosableObj.Add(this);
    }

    /// <summary>
    /// 被ダメ時にオブジェクトを点滅させる
    /// </summary>
    protected async void ShowHit()
    {
        for (int i = 0; i < _flashTime; i++)
        {
            _spriteRenderer.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur));
            _spriteRenderer.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur));
        }
    }
}
