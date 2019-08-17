using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This will play the audio of the mic in the scene
// Ensure you route the audio source to an input mixer group
// then mute that mixer group to allow this to work.
public class AudioInput : MonoBehaviour
{
    public float rmsVal;
    public float dbVal;
    public float pitchVal;

    public float minDb;
    public float baseDb;
    public float maxDb;

    public float highPitch;
    public float lowPitch;

    public float unitVolume;
    public float unitVolume1;
    public float unitVolume2;
    public float smoothUnitVolume;
    public float unitPitch;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    private AudioSource _audio;

    void Start()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        _audio.Play();
        maxDb = -1000;
        minDb = 0;
        unitVolume1 = 0;
        unitVolume2 = 0;
    }

    void Update()
    {
        AnalyzeSound();
        AnalyzeUnits();
        if (dbVal > maxDb)
        {
            maxDb = dbVal;
        }

        if (dbVal < minDb)
        {
            minDb = dbVal;
        }
        if (unitVolume > 1)
        {
            unitVolume = 1;
        }

        // Debug.Log("RMS: " + rmsVal.ToString("F2"));
        // Debug.Log(dbVal.ToString("F1") + " dB");
        // Debug.Log(pitchVal.ToString("F0") + " Hz");
    }

    void LateUpdate()
    {
        smoothUnitVolume = Mathf.Lerp(unitVolume1, unitVolume2, (0.002f * Time.time));
        unitVolume1 = smoothUnitVolume;
    }

    void AnalyzeUnits()
    {
        float dbOverBase = dbVal - baseDb;
        Debug.Log("dbOverbase: " + dbOverBase);
        unitVolume = dbOverBase / (maxDb - baseDb);
        unitVolume2 = unitVolume;
    }

    void AnalyzeSound()
    {
        _audio.GetOutputData(_samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        rmsVal = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        dbVal = 20 * Mathf.Log10(rmsVal / RefValue); // calculate dB
        if (dbVal < -160) dbVal = -160; // clamp it to -160dB min
                                        // get sound spectrum
        _audio.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchVal = freqN * (_fSample / 2) / QSamples; // convert index to frequency
    }
}

