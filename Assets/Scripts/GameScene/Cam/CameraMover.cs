using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] GameObject _player;
    Vector3 _offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = new Vector3(
            _player.transform.position.x + _offset.x,
            _player.transform.position.y + _offset.y,
            _player.transform.position.z + _offset.z);
    }
}
