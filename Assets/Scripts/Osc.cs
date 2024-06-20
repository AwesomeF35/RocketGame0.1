using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osc : MonoBehaviour
{
    Vector3 startingPositon;
       

    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 0.2f;
    

    // Start is called before the first frame update
    void Start()
    {
      
      startingPositon = transform.position;

    //  transform.position =  new Vector3(0.89f, -1.67f, 1.42f);
    }

  

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) {return;}
        float cycles = Time.time / period;
        const float tau =  Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles + tau);
        movementFactor = (rawSinWave + 1f) / 2f; 



        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPositon + offset;
    }
}
