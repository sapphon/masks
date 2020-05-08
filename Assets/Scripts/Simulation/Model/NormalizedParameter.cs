public class NormalizedParameter : INormalizedParameterChange
{
    private readonly string name;
    private readonly float value;

    public NormalizedParameter(string name, float value)
    {
        this.name = name;
        this.value = value;
    }

    public string getParameterName()
    {
        return name;
    }

    public float getParameterValue()
    {
        return value;
    }
}