using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerControls : MonoBehaviour
{
    private Rigidbody _rigidBody;
    public float jumpSize = 2.0f;
    public float jumpSizeSmooth = 2.0f;
    private bool _jumping = false;
    private bool _jumped = false;
    private AudioInput _audio;
    public float jumpThreshold = 0.5f;
    private float _jumpTime = 0.0f;
    public float JumpLength = 1.0f;
    public bool smooth = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioInput>();
    }

    void Update()
    {
        if (
            _audio.unitVolume > jumpThreshold
        )
        {
            Jump();
        }

        if (smooth)
        {
            if (_jumping)
            {
                if (Time.time > (_jumpTime + JumpLength))
                {
                    StopJump();
                }
            }

        }

        if (transform.position.y > 10)
        {
            StopJump();
        }
    }

    void FixedUpdate()
    {
        if (smooth)
        {

            if (_jumping)
            {
                if (_audio.smoothUnitVolume > 0.1f)
                {
                    _rigidBody.AddForce(Vector3.up * jumpSizeSmooth * _audio.smoothUnitVolume);
                }
            }
        }
        else
        {
            if (_jumping)
            {

                _rigidBody.AddForce(Vector3.up * jumpSize);
                StopJump();
            }
        }

    }

    void StopJump()
    {
        _jumping = false;
        _jumped = true;
        _audio.ResetSmooth();
    }

    void Jump()
    {
        if (!_jumping && !_jumped)
        {
            _jumping = true;
            _jumpTime = Time.time;
        }
    }

    public void TouchFloor()
    {
        _jumped = false;
    }

}
