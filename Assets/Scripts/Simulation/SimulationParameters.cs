using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationParameters : MonoBehaviour, IObservable<INormalizedParameterChange>
{
    public float particulateInfectionTime { get; private set; }

    public Boolean infectOthersWithinInfectionRadius { get; private set; }

    public float particulateLifetimeInAirMinimum { get; private set; }
    public float particulateLifetimeInAirMaximum { get; private set; }

    public float maskedSneezeCloudSizePercent { get; private set; }

    private float _percentPopulationMasked;
    private List<IObserver<INormalizedParameterChange>> parameterObservers;

    public float percentOfPopulationMasked
    {
        get => _percentPopulationMasked;
        private set
        {
            if (!FloatingPointComparer.isInNormalizedRange(value) || FloatingPointComparer.isCloseEnough(value, _percentPopulationMasked))
            {
                return;
            }
            else
            {
                _percentPopulationMasked = value;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        particulateInfectionTime = 0.2f;
        infectOthersWithinInfectionRadius = false;
        particulateLifetimeInAirMinimum = 10;
        particulateLifetimeInAirMaximum = 25;
        maskedSneezeCloudSizePercent = 0.25f;
        percentOfPopulationMasked = 0.2f;
        parameterObservers = new List<IObserver<INormalizedParameterChange>>();
    }

    public bool alterParameter(String key, float value)
    {
        if (key == "populationMaskPercentage")
        {
            this.percentOfPopulationMasked = value;
            notifyPopulationMaskPercentageChanged(this.percentOfPopulationMasked);
            return true;
        }

        return false;
    }

    private void notifyPopulationMaskPercentageChanged(float newValue)
    {
        parameterObservers.ForEach(obs => obs.OnNext(new NormalizedParameter("populationMaskPercentage", newValue)));
    }

    public IDisposable Subscribe(IObserver<INormalizedParameterChange> observer)
    {
        this.parameterObservers.Add(observer);
        return null;
    }
}
