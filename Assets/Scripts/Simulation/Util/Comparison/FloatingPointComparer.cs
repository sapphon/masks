using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointComparer
{
    public static Boolean isCloseEnough(float one, float two, float delta = float.Epsilon * 2)
    {
        return Math.Abs(one - two) < delta;
    }

    public static Boolean isInNormalizedRange(float one)
    {
        return one >= 0f && one <= 1f;
    }
}
