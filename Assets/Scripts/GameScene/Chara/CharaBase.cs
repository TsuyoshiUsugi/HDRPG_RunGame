using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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
    [SerializeField] protected int _hp = 1;
    [SerializeField] protected int _atk = 1;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] float _atkRate = 1;
    [SerializeField] protected float _leftLimit = -2.5f;
    [SerializeField] protected float _rightLimit = 2;
    
    BoxCollider _range;
    protected bool _isPose = false;
    public BoolReactiveProperty IsDeath = new BoolReactiveProperty();

    protected virtual void Attack() { }

    protected virtual void AutoForwardMove() { }

    public void Hit(int damage) { }

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

    private void OnEnable()
    {
        GameSceneManager.Instance.PosableObj.Add(this);
    }
}
