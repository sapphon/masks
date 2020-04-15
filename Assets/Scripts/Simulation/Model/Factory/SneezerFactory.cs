using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezerFactory : MonoBehaviour
{
    public static Sneezer makeSneezer(IMaskable sneezingThing, GameObject parent)
    {
        Sneezer added = parent.AddComponent<Sneezer>();
        added.maskable = sneezingThing;
        return added;
    }
}
