public class NormalizedParameter : INormalizedParameterChange
{
    private string name;
    private float value;

    public NormalizedParameter(string name, float value)
    {
        this.name = name;
        this.value = value;
    }

    public string getParameterName()
    {
        return this.name;
    }

    public float getParameterValue()
    {
        return this.value;
    }
}
