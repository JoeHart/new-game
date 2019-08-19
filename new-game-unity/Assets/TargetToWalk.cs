using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetToWalk : MonoBehaviour
{
    public NavMeshAgent agent;
    private AudioInput _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioInput>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        float z = 0;

        x = _audio.smoothUnitVolume * 20 - 10;
        z = _audio.smoothUnitPitch * 20 - 10;


        agent.destination = new Vector3(x, 0, z);

    }
}
