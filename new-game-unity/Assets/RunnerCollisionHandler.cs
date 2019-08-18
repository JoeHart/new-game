using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCollisionHandler : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private RunnerControls _runnerControls;
    public AutoRunnerGameLogic gameLogic;

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
            case "Obstacle(Clone)":
                HandleObstacleCollision();
                break;

        }
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.name)
        {
            case "ObstacleOver":
                HandleObstacleOverCollision();
                break;
        }
    }

    void HandleFloorCollision()
    {
        _runnerControls.TouchFloor();
    }

    void HandleObstacleCollision()
    {
        gameLogic.EndGame();
    }

    void HandleObstacleOverCollision()
    {
        gameLogic.JumpOver();
    }
}
