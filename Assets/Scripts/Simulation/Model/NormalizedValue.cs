public class NormalizedValue : INormalizedValueChange
{
    private readonly string name;
    private readonly float value;

    public NormalizedValue(string name, float value)
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