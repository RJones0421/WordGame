using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPulse : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(5f, 0f, 0f);
    [SerializeField] float period = 2f;

    float movementFactor; //0 unmoved, 1 fully moved
    float startingXPos;

    private void Start()
    {
        startingXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = new Vector3(startingXPos + offset.x, transform.position.y, transform.position.z);
    }
}