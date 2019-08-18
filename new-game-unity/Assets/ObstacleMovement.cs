using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public AutoRunnerGameLogic gameLogic;
    void FixedUpdate()
    {
        if (gameLogic.started)
        {
            transform.position = new Vector3(transform.position.x - (gameLogic.obstacleSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            if (transform.position.x < -15)
            {
                Destroy(gameObject);
            }
        }
    }
}
