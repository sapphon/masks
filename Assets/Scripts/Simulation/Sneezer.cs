﻿using UnityEngine;

public class Sneezer : MonoBehaviour
{
    private float nextSneezeTime;


    private float rateOfSneezeMinimum;

    public IMaskable maskable { get; set; }

    private SimulationParameters simulationParameters;

    // Start is called before the first frame update
    private void Start()
    {
        simulationParameters = GameObject.FindObjectOfType<SimulationParameters>();
        rateOfSneezeMinimum = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        checkSneeze();
    }

    private void checkSneeze()
    {
        if (Time.time > nextSneezeTime)
        {
            sneeze();
            setTimeOfNextSneeze();
        }
    }

    private void sneeze()
    {
        var thisTransform = gameObject.transform;
        if (maskable != null && maskable.isMasked)
            ParticulateFactory.makeMaskedParticulateCloud(thisTransform, getSneezeDirection());
        else
            ParticulateFactory.makeParticulateCloud(thisTransform, getSneezeDirection());
    }

    private void setTimeOfNextSneeze()
    {
        nextSneezeTime += Random.Range(rateOfSneezeMinimum, simulationParameters.particulateGenerationDelayAvg + rateOfSneezeMinimum);
    }

    private Quaternion getSneezeDirection()
    {
        var angle = Random.Range(-135f, 135f);
        return Quaternion.AngleAxis(angle, gameObject.transform.up);
    }
}