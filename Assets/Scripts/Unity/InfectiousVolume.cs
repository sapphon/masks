using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectiousVolume : ParameterDrivenBehavior
{
    private Dictionary<GameObject, float> objectsEntered;

    void Awake()
    {
        objectsEntered = new Dictionary<GameObject, float>();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.objectsEntered.Add(other.gameObject, Time.timeSinceLevelLoad);
    }

    private void OnTriggerExit(Collider other)
    {
        Person exiter = other.gameObject.GetComponent<Person>();
        if (exiter != null)
        {
            float timeSpentInArea = Time.timeSinceLevelLoad - this.objectsEntered[other.gameObject];
            if (timeSpentInArea > this.simulationParameters.particulateInfectionTime * (exiter.isMasked ? 1.33 : 1))
            {
                exiter.infect();
            }
        }
        this.objectsEntered.Remove(other.gameObject);
    }
}
