﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExampleMovement : MonoBehaviour
{
    public AudioInput _audioInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3((2 * _audioInput.smoothUnitPitch) - 1, (2 * _audioInput.smoothUnitVolume) - 1, 0);
        transform.position = position;

    }
}
