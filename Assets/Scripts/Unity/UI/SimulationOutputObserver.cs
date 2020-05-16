using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationOutputObserver : MonoBehaviour
{
    
    public string parameterRepresented;

    protected Simulation simulation;
    protected Text textRepresentingOutput;
    void Start()
    {
        this.simulation = GameObject.FindObjectOfType<Simulation>();
        this.textRepresentingOutput = this.GetComponentInChildren<Text>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
