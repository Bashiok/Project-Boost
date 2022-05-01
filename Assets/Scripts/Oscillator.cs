using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movmentVectro;
    float movmentFactor;
    [SerializeField] [Range(0,1000)] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycle = Time.time / period;
        
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycle * tau);

        movmentFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movmentVectro * movmentFactor;
        transform.position = startingPosition + offset;
    }
}
