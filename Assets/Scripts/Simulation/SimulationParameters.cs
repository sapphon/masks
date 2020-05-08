using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulationParameters : MonoBehaviour, IObservable<INormalizedParameterChange>
{
    private float _percentPopulationMasked;
    private List<IObserver<INormalizedParameterChange>> parameterObservers;
    public float particulateInfectionTime { get; private set; }

    public bool infectOthersWithinInfectionRadius { get; private set; }

    public float particulateLifetimeInAirAvg { get; private set; }

    public float maskedSneezeCloudSizePercent { get; private set; }

    public float infectionLatencyTime { get; private set; }

    public float infectionContagionTime { get; private set; }

    public float percentOfPopulationMasked
    {
        get => _percentPopulationMasked;
        private set
        {
            if (!FloatingPointComparer.isInNormalizedRange(value) ||
                FloatingPointComparer.isCloseEnough(value, _percentPopulationMasked))
                return;
            _percentPopulationMasked = value;
        }
    }

    public IDisposable Subscribe(IObserver<INormalizedParameterChange> observer)
    {
        parameterObservers.Add(observer);
        return null;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        particulateInfectionTime = 0.2f;
        infectOthersWithinInfectionRadius = false;
        particulateLifetimeInAirAvg = 20f;
        maskedSneezeCloudSizePercent = 0.25f;
        percentOfPopulationMasked = 0.2f;
        infectionContagionTime = 20f;
        infectionLatencyTime = 5f;
        parameterObservers = new List<IObserver<INormalizedParameterChange>>();
    }

    public bool alterParameter(string key, float value)
    {
        if (key == "populationMaskPercentage")
        {
            percentOfPopulationMasked = value;
            notifyObserversStatisticChanged(new NormalizedParameter("populationMaskPercentage",
                percentOfPopulationMasked));
            return true;
        }

        if (key == "particulateLifetimeAvg")
        {
            particulateLifetimeInAirAvg = 50f * value;
            notifyObserversStatisticChanged(new NormalizedParameter("particulateLifetimeAvg",
                particulateLifetimeInAirAvg));
            return true;
        }

        if (key == "infectionContagionTime")
        {
            infectionContagionTime = 50f * value;
            notifyObserversStatisticChanged(new NormalizedParameter("infectionContagionTime", infectionContagionTime));
            return true;
        }

        if (key == "infectionLatencyTime")
        {
            infectionLatencyTime = 50f * value;
            notifyObserversStatisticChanged(new NormalizedParameter("infectionLatencyTime", infectionLatencyTime));
            return true;
        }

        return false;
    }

    private void notifyObserversStatisticChanged(NormalizedParameter toNotifyOf)
    {
        parameterObservers.ForEach(obs => obs.OnNext(toNotifyOf));
    }
}