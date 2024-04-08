using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)]float movementFactor;
    [SerializeField] float period = 2f;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period == 0) { return; }
        float cycles = Time.time / period;  // continually growing over time

        const float tau = Mathf.PI * 2;     // constat value of 6.283
        float rawSignWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSignWave + 1f) / 2f;  // recalculated 0 to 1.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
