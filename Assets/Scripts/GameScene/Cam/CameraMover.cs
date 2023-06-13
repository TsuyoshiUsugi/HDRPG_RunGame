using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    [Header("設定値")]
    [SerializeField] float _shakeStrength = 1.0f;
    [SerializeField] int _shakeTime = 2;

    GameObject _player;
    Vector3 _offset = Vector3.zero;
    bool _isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameSceneManager.Instance.Player.gameObject;
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_isHit) return;

        this.transform.position = new Vector3(
            _player.transform.position.x + _offset.x,
            _player.transform.position.y + _offset.y,
            _player.transform.position.z + _offset.z);
    }

    /// <summary>
    /// プレイヤーがダメージを受けた時に呼ばれる
    /// </summary>
    public void OnHitCam(float dur)
    {
        _isHit = true;
        Debug.Log("Hit");
        Camera.main.DOShakePosition(dur, _shakeStrength, _shakeTime).OnComplete(() => _isHit = false).SetLink(Camera.main.gameObject);
    }
}
