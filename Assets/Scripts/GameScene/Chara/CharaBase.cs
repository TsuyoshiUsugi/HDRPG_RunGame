using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

/// <summary>
/// �G�APlayer�A�{�X�̃x�[�X�ƂȂ�N���X
/// 
/// [�ϐ�]
/// �EHP
/// �EATK
/// �ESpeed
/// �EATKRate
/// 
/// [�֐�]
/// �E�U��
/// �E�ړ�
/// �EHit
/// �E�ʒu�C��
/// 
/// [interface]
/// �E�q�b�g
/// �E�|�[�Y
/// </summary>
public class CharaBase : MonoBehaviour, IHit, IPosable
{
    [Header("�ݒ�l")]
    [SerializeField] protected IntReactiveProperty _hp = new IntReactiveProperty(1);
    [SerializeField] protected int _atk = 1;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected float _atkRate = 1;
    [SerializeField] GameObject _attackHitBox;


    int _flashTime = 2;
    float _flashDur = 0.1f;
    protected bool _isHit = false;
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;
    protected float _leftLimit = -2.5f;
    protected float _rightLimit = 2;
    protected float _restCooldownTime = 0;
    protected HitBox _hitBox;
    
    [SerializeField] protected BoolReactiveProperty _isDeath = new BoolReactiveProperty(false);
    protected BoolReactiveProperty _isPose = new BoolReactiveProperty(false);

    public float AtkRate => _atkRate;
    public IntReactiveProperty Hp => _hp;
    public BoolReactiveProperty IsDeath => _isDeath;

    void Awake()
    {
        TryGetComponent(out _animator);
        TryGetComponent(out _spriteRenderer);
        if (_attackHitBox) _attackHitBox.TryGetComponent(out _hitBox);

        _leftLimit = GameSceneManager.Instance.GetFieldInfo().leftSide;
        _rightLimit = GameSceneManager.Instance.GetFieldInfo().rightSide;
        this.UpdateAsObservable().Where(_ => _isPose.Value == false).Subscribe(_ => AutoForwardMove()).AddTo(this);
    }

    public virtual void Attack() { }

    protected virtual void AutoForwardMove() {}

    public void Hit(int damage) { }

    /// <summary>
    /// �|�[�Y����
    /// </summary>
    /// <param name="isPoseing"></param>
    public void Pose(bool isPoseing)
    {
        _isPose.Value = isPoseing;
        if (_animator) _animator.enabled = !isPoseing;
    }

    /// <summary>
    /// �ݒ肳��Ă���̈�O�ɂłȂ��悤�ɂ���
    /// </summary>
    protected void ResetPos()
    {
        if (transform.position.x > _rightLimit) transform.position = new Vector3(_rightLimit, transform.position.y, transform.position.z);
        if (transform.position.x < _leftLimit) transform.position = new Vector3(_leftLimit, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// ��������I�u�W�F�N�g��scene�J�n���ɃQ�[���}�l�[�W���[�ɒǉ�
    /// </summary>
    private void OnEnable()
    {
        GameSceneManager.Instance.PosableObj.Add(this);
    }

    /// <summary>
    /// ��_�����ɃI�u�W�F�N�g��_�ł�����
    /// </summary>
    protected async UniTask ShowHit()
    {
        if (_spriteRenderer == null) return;
        var token = this.GetCancellationTokenOnDestroy();

        for (int i = 0; i < _flashTime; i++)
        {
            _spriteRenderer.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur), cancellationToken: token);
            if (_spriteRenderer == null) return;
            _spriteRenderer.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur), cancellationToken: token);
        }
    }
}
