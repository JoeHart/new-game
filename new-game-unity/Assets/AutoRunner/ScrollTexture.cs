using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    public Renderer rend;
    public AutoRunnerGameLogic gameLogic;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if (gameLogic.started)
        {
            float offset = Time.time * gameLogic.obstacleSpeed * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}
