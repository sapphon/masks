using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulationParameters : MonoBehaviour, IObservable<INormalizedValueChange>
{
    private float _percentPopulationMasked;
    private List<IObserver<INormalizedValueChange>> parameterObservers;
    public float particulateInfectionTime { get; private set; }

    public bool infectOthersWithinInfectionRadius { get; private set; }

    public float particulateLifetimeInAirAvg { get; private set; }

    public float maskedSneezeCloudSizePercent { get; private set; }

    public float infectionLatencyTime { get; private set; }

    public float infectionContagionTime { get; private set; }
    
    public float personWalkSpeedAvg { get; private set; }
    
    public float particulateGenerationDelayAvg { get; private set; }

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

    public IDisposable Subscribe(IObserver<INormalizedValueChange> observer)
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
        personWalkSpeedAvg = 7.5f;
        particulateGenerationDelayAvg = 15f;
        parameterObservers = new List<IObserver<INormalizedValueChange>>();
    }

    public bool alterParameter(string key, float value)
    {
        if (key == "populationMaskPercentage")
        {
            percentOfPopulationMasked = value;
            notifyObserversStatisticChanged(new NormalizedValue("populationMaskPercentage",
                percentOfPopulationMasked));
            return true;
        }
        else if (key == "particulateLifetimeAvg")
        {
            particulateLifetimeInAirAvg = 50f * value;
            notifyObserversStatisticChanged(new NormalizedValue("particulateLifetimeAvg",
                particulateLifetimeInAirAvg));
            return true;
        }
        
        else if (key == "particulateGenerationDelayAvg")
        {
            particulateGenerationDelayAvg = 20f * value;
            notifyObserversStatisticChanged(new NormalizedValue("particulateGenerationDelayAvg",
                particulateGenerationDelayAvg));
            return true;
        }

        else if (key == "infectionContagionTime")
        {
            infectionContagionTime = 50f * value;
            notifyObserversStatisticChanged(new NormalizedValue("infectionContagionTime", infectionContagionTime));
            return true;
        }

        else if (key == "infectionLatencyTime")
        {
            infectionLatencyTime = 50f * value;
            notifyObserversStatisticChanged(new NormalizedValue("infectionLatencyTime", infectionLatencyTime));
            return true;
        }
        else if (key == "personWalkSpeedAvg")
        {
            personWalkSpeedAvg = 10f * value;
            notifyObserversStatisticChanged(new NormalizedValue("personWalkSpeedAvg", personWalkSpeedAvg));
            return true;
        }
        return false;
    }

    private void notifyObserversStatisticChanged(NormalizedValue toNotifyOf)
    {
        parameterObservers.ForEach(obs => obs.OnNext(toNotifyOf));
    }
}