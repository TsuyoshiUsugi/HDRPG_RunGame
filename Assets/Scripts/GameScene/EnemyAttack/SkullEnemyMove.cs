using UnityEngine;
using DG.Tweening;

public class SkullEnemyMove : MonoBehaviour, IEnemyMove
{
    [SerializeField] float _time = 0;

    public void EnemyMove(float speed)
    {
        transform.DOLocalMoveX();
        this.transform.position += new Vector3 (Mathf.Sign(_time), 0, speed) * Time.deltaTime;
    }
}
