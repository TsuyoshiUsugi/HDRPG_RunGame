using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWallMover : MonoBehaviour
{
    GameObject _player;
    Vector3 _offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameSceneManager.Instance.Player.gameObject;
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            _player.transform.position.z + _offset.z);
    }
}
