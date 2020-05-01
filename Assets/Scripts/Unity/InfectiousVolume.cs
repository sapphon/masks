using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfectiousVolume : ParameterDrivenBehavior
{
    private Dictionary<GameObject, float> objectsWithinVolume;
    private float timeToDie;

    void Awake()
    {
        objectsWithinVolume = new Dictionary<GameObject, float>();
    }

    internal override void Start()
    {
        base.Start();
        this.timeToDie = Time.time + this.simulationParameters.particulateLifetimeInAirAvg + Random.Range(-simulationParameters.particulateLifetimeInAirAvg*.2f, simulationParameters.particulateLifetimeInAirAvg*.2f);
    }

    void Update()
    {
        if (Time.time >= timeToDie)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.objectsWithinVolume.Add(other.gameObject, Time.timeSinceLevelLoad);
    }

    private void OnTriggerExit(Collider other)
    {
        Person exiter = other.gameObject.GetComponent<Person>();
        if (exiter != null)
        {
            float timeSpentInArea = Time.timeSinceLevelLoad - this.objectsWithinVolume[other.gameObject];
            if (timeSpentInArea > this.simulationParameters.particulateInfectionTime * (exiter.isMasked ? 1.33 : 1))
            {
                exiter.infect();
            }
        }
        this.objectsWithinVolume.Remove(other.gameObject);
    }
}
