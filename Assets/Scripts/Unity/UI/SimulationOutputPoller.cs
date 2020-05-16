using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimulationOutputPoller : MonoBehaviour
{
    private Simulation simulation;
    private Text textToKeepUpdated;
    public string valueToPoll;

    // Start is called before the first frame update
    void Start()
    {
        this.simulation = GameObject.FindObjectOfType<Simulation>();
        this.textToKeepUpdated = this.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update() => this.textToKeepUpdated.text = simulation.getValue(valueToPoll);
}
