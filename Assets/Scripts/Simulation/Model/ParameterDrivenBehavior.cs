using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterDrivenBehavior : MonoBehaviour
{
    protected SimulationParameters simulationParameters;
    
    internal virtual void Start()
    {
        populateSimulationParameterObject();
    }

    private void populateSimulationParameterObject()
    {
        this.simulationParameters = GameObject.FindObjectOfType<SimulationParameters>();
    }
}
