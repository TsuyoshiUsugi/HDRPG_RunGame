using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

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
    protected BoolReactiveProperty _isDeath = new BoolReactiveProperty(false);


    protected float _leftLimit = -2.5f;
    protected float _rightLimit = 2;
    protected float _currentAtkDur = 0;
    int _flashTime = 2;
    float _flashDur = 0.1f;
    
    SpriteRenderer _spriteRenderer;
    protected bool _isPose = false;

    public IntReactiveProperty Hp => _hp;
    public BoolReactiveProperty IsDeath => _isDeath;

    protected void Start()
    {
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        _leftLimit = GameSceneManager.Instance.GetFieldInfo().leftSide;
        _rightLimit = GameSceneManager.Instance.GetFieldInfo().rightSide;
    }

    public virtual void Attack() { }

    protected virtual void AutoForwardMove() { }

    public void Hit(int damage) { }

    /// <summary>
    /// �|�[�Y����
    /// </summary>
    /// <param name="isPoseing"></param>
    public void Pose(bool isPoseing)
    {
        _isPose = isPoseing;
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
    protected async void ShowHit()
    {
        if (_spriteRenderer == null) return;

        for (int i = 0; i < _flashTime; i++)
        {
            _spriteRenderer.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur));
            if (_spriteRenderer == null) return;
            _spriteRenderer.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur));
        }
    }
}
