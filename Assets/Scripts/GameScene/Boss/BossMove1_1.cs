using UnityEngine;

/// <summary>
/// 1-1�ŏo������{�X�̓����̃X�N���v�g
/// ���E�Ɉړ����Ĉ�莞�Ԍ�,�n�ʂɗ���
/// ������J��Ԃ�
/// </summary>
public class BossMove1_1 : MonoBehaviour, IEnemyMove
{
    [Header("�ݒ�l")]
    [SerializeField] float _speed = 1;
    [SerializeField] BossState _currentState = BossState.Float;
    [SerializeField] float _ground = 0;
    [SerializeField] float _switchStateTime = 10;

    float _currentStateTime = 0;
    float _originPos = 0;
    float _leftSide = 0;
    float _rightSide = 0;
    BoxCollider _HitCollider;
    Vector3 _originColSize = Vector3.zero;
    float _restStateColSizeZ = 5;

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// �ϐ��̏���������
    /// </summary>
    private void Initialize()
    {
        _HitCollider = GetComponent<BoxCollider>();
        _originColSize = _HitCollider.size;
        _originPos = transform.position.y;
        _currentState = BossState.Float;
        _leftSide = GameSceneManager.Instance.GetFieldInfo().leftSide;
        _rightSide = GameSceneManager.Instance.GetFieldInfo().rightSide;
    }

    public void EnemyMove(float speed)
    {
        if (_currentState == BossState.Float)
        {
            FloatState();

        }
        else if (_currentState == BossState.Rest)
        {
            RestState();
        }
    }

    /// <summary>
    /// �����Ă���Ƃ��̓����̊֐�
    /// ���E�Ɉړ�
    /// </summary>
    private void FloatState()
    {
        if (_currentStateTime <= _switchStateTime)
        {
            _currentStateTime += Time.deltaTime;

            transform.position += Vector3.right * _speed * Time.deltaTime;

            if (transform.position.x <= _leftSide) _speed = -_speed;
            if (transform.position.x >= _rightSide) _speed = -_speed;
        }
        else
        {
            _currentStateTime = 0;
            _currentState = BossState.Rest;
        }
    }

    /// <summary>
    /// �x�����̓����̊֐�
    /// </summary>
    private void RestState()
    {

        if (_currentStateTime <= _switchStateTime)
        {
            _currentStateTime += Time.deltaTime;
            transform.position -= Vector3.down * _speed * Time.deltaTime;
            _HitCollider.size = new Vector3(_HitCollider.size.x, _HitCollider.size.y, _restStateColSizeZ);

            if (transform.position.y <= _ground) transform.position =
                    new Vector3(transform.position.x, _ground, transform.position.z);
        }
        else
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;

            if (transform.position.y >= _originPos)
            {
                transform.position = new Vector3(transform.position.x, _originPos, transform.position.z);
                _currentStateTime = 0;
                _currentState = BossState.Float;
                _HitCollider.size = _originColSize;
            }
        }
    }

    enum BossState
    {
        Float,
        Rest,
    }
}
