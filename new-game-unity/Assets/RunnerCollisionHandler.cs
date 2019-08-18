using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCollisionHandler : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private RunnerControls _runnerControls;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _runnerControls = GetComponent<RunnerControls>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        switch (collision.collider.name)
        {
            case "floor":
                HandleFloorCollision();
                break;
        }
    }

    void HandleFloorCollision()
    {
        _runnerControls.TouchFloor();
    }
}
