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
        simulationParameters = FindObjectOfType<SimulationParameters>();
    }
}