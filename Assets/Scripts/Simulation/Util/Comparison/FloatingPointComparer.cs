using System;

public class FloatingPointComparer
{
    public static bool isCloseEnough(float one, float two, float delta = float.Epsilon * 2)
    {
        return Math.Abs(one - two) < delta;
    }

    public static bool isInNormalizedRange(float one)
    {
        return one >= 0f && one <= 1f;
    }

    public static bool isBetweenInclusive(float presumedBefore, float value, float presumedAfter)
    {
        return presumedBefore <= value && value <= presumedAfter;
    }
}