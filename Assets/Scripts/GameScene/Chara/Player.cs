using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : CharaBase
{
    Vector3 _moveDir = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        transform.position += _moveDir * _speed;
    }
}
