using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderObserver : MonoBehaviour
{
    public string parameterRepresented;
    protected Slider slider;
    protected SimulationParameters simulationParameters;

    
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
        simulationParameters = GameObject.FindObjectOfType<SimulationParameters>();
        setInitialValueFromParameters();
        registerObserver();
        
    }

    protected virtual bool floatValueChanged()
    {
        return simulationParameters.alterParameter(parameterRepresented, slider.normalizedValue);
    }

    protected virtual void setInitialValueFromParameters()
    {
        this.slider.value = this.simulationParameters.percentOfPopulationMasked;
    }

    protected virtual void registerObserver()
    {
        slider.onValueChanged.AddListener(delegate { this.floatValueChanged();});
    }
}
