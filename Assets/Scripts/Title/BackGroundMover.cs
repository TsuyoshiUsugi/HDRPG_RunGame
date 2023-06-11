using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景の画像を動かすスクリプト
/// </summary>
public class BackGroundMover : MonoBehaviour
{
    [SerializeField] List<GameObject> _backImages;
    [SerializeField] float _speed = 10;
    Vector3 _dir = Vector3.left;
    Vector3 _start = Vector3.zero;
    Vector3 _diff = Vector3.zero;
    int _currentFrontObj = 0;

    private void Start()
    {
        _start = _backImages[0].transform.position;
        _diff = _backImages[1].transform.position - _backImages[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in _backImages)
        {
            go.transform.position += _dir * _speed * Time.deltaTime;
        }

        if (_backImages[_currentFrontObj + 1].transform.position.x < _start.x)
        {
            _backImages[_currentFrontObj].transform.position = _start + _diff;

            var temp = _backImages[0];
            _backImages[0] = _backImages[1];
            _backImages[1] = temp;
        }
    }
}
