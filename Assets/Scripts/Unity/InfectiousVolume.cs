using System.Collections.Generic;
using UnityEngine;

public class InfectiousVolume : ParameterDrivenBehavior
{
    private Dictionary<GameObject, float> objectsWithinVolume;
    private float timeToDie;

    private void Awake()
    {
        objectsWithinVolume = new Dictionary<GameObject, float>();
    }

    internal override void Start()
    {
        base.Start();
        timeToDie = Time.time + simulationParameters.particulateLifetimeInAirAvg + Random.Range(
                        -simulationParameters.particulateLifetimeInAirAvg * .2f,
                        simulationParameters.particulateLifetimeInAirAvg * .2f);
    }

    private void Update()
    {
        if (Time.time >= timeToDie) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsWithinVolume.Add(other.gameObject, Time.timeSinceLevelLoad);
    }

    private void OnTriggerExit(Collider other)
    {
        var exiter = other.gameObject.GetComponent<Person>();
        if (exiter != null && !exiter.isRecovered)
        {
            var timeSpentInArea = Time.timeSinceLevelLoad - objectsWithinVolume[other.gameObject];
            if (timeSpentInArea > simulationParameters.particulateInfectionTime * (exiter.isMasked ? 1.33 : 1))
                exiter.infect();
        }

        objectsWithinVolume.Remove(other.gameObject);
    }
}