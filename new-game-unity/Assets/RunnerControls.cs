using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerControls : MonoBehaviour
{
    private Rigidbody _rigidBody;
    public float jumpSize = 2.0f;
    private bool _jumping = false;
    private bool _jumped = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("trigger jump");
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (_jumping)
        {
            _rigidBody.AddForce(Vector3.up * jumpSize);
            _jumped = true;
            _jumping = false;
        }
    }

    void Jump()
    {
        if (!_jumping && !_jumped)
        {
            _jumping = true;
        }
    }

    public void TouchFloor()
    {
        _jumped = false;
    }

}
