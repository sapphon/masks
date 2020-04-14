using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationParameters : MonoBehaviour
{
    public float particulateInfectionTime { get; private set; }
    public Boolean infectOthersWithinInfectionRadius { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        particulateInfectionTime = 0.2f;
        infectOthersWithinInfectionRadius = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
