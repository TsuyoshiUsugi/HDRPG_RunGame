using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�Ɍ������Ĕ��ł���΂̋ʂ̃I�u�W�F�N�g
/// </summary>
public class Flame : ObstacleBase
{
    Vector3 _dir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _dir = GameSceneManager.Instance.Player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _dir * _speed * Time.deltaTime;

        if (GameSceneManager.Instance.GetFieldInfo().leftSide > transform.position.x) Destroy(gameObject);
        if (GameSceneManager.Instance.GetFieldInfo().rightSide < transform.position.x) Destroy(gameObject);
        if (GameSceneManager.Instance.Player.transform.position.z > transform.position.z) Destroy(gameObject);
    }

   
}
