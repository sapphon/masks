using UnityEngine;
using UnityEngine.UI;

public class SliderObserver : MonoBehaviour
{
    public string parameterRepresented;
    protected SimulationParameters simulationParameters;
    protected Slider slider;


    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        simulationParameters = FindObjectOfType<SimulationParameters>();
        setInitialValueFromParameters();
        registerObserver();
    }

    protected virtual bool floatValueChanged()
    {
        return simulationParameters.alterParameter(parameterRepresented, slider.normalizedValue);
    }

    protected virtual void setInitialValueFromParameters()
    {
        slider.value = simulationParameters.percentOfPopulationMasked;
    }

    protected virtual void registerObserver()
    {
        slider.onValueChanged.AddListener(delegate { floatValueChanged(); });
    }
}