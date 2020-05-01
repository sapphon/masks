using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationParameters : MonoBehaviour, IObservable<INormalizedParameterChange>
{
    public float particulateInfectionTime { get; private set; }

    public Boolean infectOthersWithinInfectionRadius { get; private set; }

    public float particulateLifetimeInAirAvg { get; private set; }

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
        particulateLifetimeInAirAvg = 20f;
        maskedSneezeCloudSizePercent = 0.25f;
        percentOfPopulationMasked = 0.2f;
        parameterObservers = new List<IObserver<INormalizedParameterChange>>();
    }

    public bool alterParameter(String key, float value)
    {
        if (key == "populationMaskPercentage")
        {
            this.percentOfPopulationMasked = value;
            notifyObserversStatisticChanged(new NormalizedParameter("populationMaskPercentage",this.percentOfPopulationMasked));
            return true;
        }
        else if (key == "particulateLifetimeAvg")
        {
            this.particulateLifetimeInAirAvg = 50f * value;
            notifyObserversStatisticChanged(new NormalizedParameter("particulateLifetimeAvg",this.particulateLifetimeInAirAvg));
            return true;
        }

        return false;
    }

    private void notifyObserversStatisticChanged(NormalizedParameter toNotifyOf)
    {
        parameterObservers.ForEach(obs => obs.OnNext(toNotifyOf));
    }

    public IDisposable Subscribe(IObserver<INormalizedParameterChange> observer)
    {
        this.parameterObservers.Add(observer);
        return null;
    }
}
