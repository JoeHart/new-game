using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCollisionHandler : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private RunnerControls _runnerControls;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _runnerControls = GetComponent<RunnerControls>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.name)
        {
            case "floor":
                HandleFloorCollision();
                break;
            case "Obstacle":
                HandleObstacleCollision();
                break;
        }
    }

    void HandleFloorCollision()
    {
        _runnerControls.TouchFloor();
    }

    void HandleObstacleCollision()
    {

    }
}
