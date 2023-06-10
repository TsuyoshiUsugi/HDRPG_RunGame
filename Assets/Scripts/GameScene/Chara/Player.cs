using UnityEngine;

public class Player : CharaBase
{
    [Header("�Q��")]
    [SerializeField] GameObject _attackHitBox;

    public float AtkRate => _atkRate;
    Vector3 _moveDir = Vector3.forward;
    float _moveLimit = 10;

    private void Start()
    {
        _moveLimit = GameSceneManager.Instance.Goal;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPose) return;

        _currentAtkDur -= Time.deltaTime;
        AutoForwardMove();
    }

    /// <summary>
    /// ����ɑO�ɐi�ޏ���
    /// </summary>
    protected override void AutoForwardMove()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;

        if (transform.position.z > _moveLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _moveLimit);
        }
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

    public new void Hit(int damage)
    {
        Debug.Log("Hit");

        ShowHit();
        _hp -= damage;

        if (_hp <= 0)
        {
            _hp = 0;
            IsDeath.Value = true;
        }
    }

    public override void Attack()
    {
        if (_isPose) return;
        //if (_currentAtkDur > 0) return;

        Debug.Log("Attack");
        Collider[] enemyCol = Physics.OverlapBox(_attackHitBox.transform.position, _attackHitBox.transform.lossyScale);

        foreach (Collider col in enemyCol)
        {
            col.TryGetComponent<Enemy>(out Enemy enemy);
            if (enemy) enemy.Hit(_atk);
        }

        _currentAtkDur = _atkRate;
    }

#if UNITY_EDITOR
    /// <summary>
    /// �U���\�͈͂̉���
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackHitBox.transform.position, _attackHitBox.transform.lossyScale);
    }
#endif
}
