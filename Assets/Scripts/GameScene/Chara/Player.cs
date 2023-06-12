using UniRx;
using UnityEngine;

public class Player : CharaBase
{
    [Header("参照")]
    [SerializeField] GameObject _attackHitBox;

    float _goalPosZ = 10;
    Vector3 _moveDir = Vector3.forward;
    

    private void Start()
    {
        _goalPosZ = GameSceneManager.Instance.Goal;
    }

    /// <summary>
    /// 勝手に前に進む処理
    /// </summary>
    protected override void AutoForwardMove()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;

        if (transform.position.z > _goalPosZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _goalPosZ);
        }
    }

    /// <summary>
    /// Playerの入力により左右に進む処理
    /// </summary>
    /// <param name="isLeft">trueなら左、falseなら右</param>
    public void LeftRightMove(bool isLeft)
    {
        if (_isPose.Value) return;
        if (isLeft) transform.position += Vector3.left * _speed * Time.deltaTime;
        if (!isLeft) transform.position -= Vector3.left * _speed * Time.deltaTime;

        ResetPos();
    }

    public new async void Hit(int damage)
    {
        if (GameSceneManager.Instance.IsClear) return;
        if (_isHit) return;

        _hp.Value -= damage;

        if (_hp.Value <= 0)
        {
            _hp.Value = 0;
            _isDeath.Value = true;
        }

        _isHit = true;
        await ShowHit();
        _isHit = false;

    }

    public override void Attack()
    {
        if (_isPose.Value) return;

        Debug.Log("Attack");
        Collider[] enemyCol = Physics.OverlapBox(_attackHitBox.transform.position, _attackHitBox.transform.lossyScale);

        foreach (Collider col in enemyCol)
        {
            col.TryGetComponent(out Enemy enemy);
            if (enemy) enemy.Hit(_atk);
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// 攻撃可能範囲の可視化
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackHitBox.transform.position, _attackHitBox.transform.lossyScale);
    }
#endif
}
