using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoRunnerGameLogic : MonoBehaviour
{
    public int jumpedOver = 0;
    public bool lost = false;
    public bool started = false;
    public float obstacleInterval = 1.0f;
    private float lastSpawnTime = 0.0f;
    private float nextSpawnTime = 0.0f;
    public GameObject obstaclePrefab;
    public Text displayText;
    public float obstacleSpeed = 5.0f;
    public float obstacleMaxSpeed = 0.5f;
    public float obstacleMinGap = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Time.time > nextSpawnTime)
            {
                SpawnObstacle();
            }
        }

    }

    public void EndGame()
    {
        lost = true;
        started = false;
        GetComponent<AudioSource>().Play();
        displayText.text = "Game Over\n You got " + jumpedOver;
    }

    void updateUI()
    {
        displayText.text = "SCORE: " + jumpedOver;
    }

    public void JumpOver()
    {
        jumpedOver++;
        updateUI();
    }

    void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(12, -0.5f, 1), Quaternion.Euler(0, 0, 45));
        lastSpawnTime = Time.time;
        nextSpawnTime = lastSpawnTime + Random.Range(obstacleMinGap, obstacleInterval);
        obstacle.GetComponent<ObstacleMovement>().gameLogic = this;
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
