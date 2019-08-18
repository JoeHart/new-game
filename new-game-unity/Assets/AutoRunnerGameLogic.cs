using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunnerGameLogic : MonoBehaviour
{
    public int jumpedOver = 0;
    public bool lost = false;
    public float obstacleInterval = 1.0f;
    private float lastSpawnTime = 0.0f;
    private float nextSpawnTime = 0.0f;
    public GameObject obstaclePrefab;
    public float obstacleSpeed = 0.05f;
    public float obstacleMaxSpeed = 0.5f;
    public float obstacleMinGap = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(12, -0.5f, 1), Quaternion.Euler(0, 0, 45));
        lastSpawnTime = Time.time;
        nextSpawnTime = lastSpawnTime + Random.Range(obstacleMinGap, obstacleInterval);
        obstacle.GetComponent<ObstacleMovement>().speed = obstacleSpeed;
        if (obstacleSpeed > obstacleMaxSpeed)
        {
            obstacleSpeed = obstacleSpeed * 1.1f;
        }

        if (obstacleMinGap > 0.2f)
        {
            obstacleMinGap = obstacleMinGap * 0.9f;
        }

        float shouldScale = Random.Range(0.0f, 1.0f);
        if (shouldScale > 0.7f)
        {
            obstacle.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
